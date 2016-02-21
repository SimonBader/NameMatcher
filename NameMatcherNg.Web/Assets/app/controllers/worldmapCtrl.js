angular.module('worldmap', [])
    .controller('worldmapCtrl', ['$scope', '$http', function ($scope, $http) {
        $scope.$watch('highlightedCountryCodes', function () {
            $('.timezone-map polygon').attr('class', '');
            for (index in $scope.highlightedCountryCodes) {
                $('.timezone-map polygon[data-country="' + $scope.highlightedCountryCodes[index] + '"]').attr('class', 'highlighted');
            }
        });

        /**
         * [generateMap create element dynamically]
         * @param  {[type]} options [depanding on option it will create e]
         * @return {[type]}         [description]
         */
        this.initialize = function (element, options) {
            this.element = element;
            this.countryCodes = new FixedQueue(2);

            var polygon = [],
                option = [],
                quickLink = [],
                containerArr = [],
                timezone = this.timeZoneValue;
            for (var index in timezone) {
                polygon.push(this.generateElement('polygon', {
                    'data-timezone': timezone[index].timezone,
                    'data-country': timezone[index].country,
                    'data-pin': timezone[index].pin,
                    'data-offset': timezone[index].offset,
                    'points': timezone[index].points,
                    'data-zonename': ((options.dayLightSaving) ? (moment().tz(timezone[index].timezone).zoneName()) : (timezone[index].zoneName))
                }, false, true));
                option.push(this.generateElement('option', {
                    'value': timezone[index].timezone
                }, timezone[index].timezone + " (" + ((options.dayLightSaving) ? (moment().tz(timezone[index].timezone).zoneName()) : (timezone[index].zoneName)) + ")"));
            }
            if (options.selectBox) {
                var select = this.generateElement('select', {
                    'class': 'btn btn-default dropdown-toggle',
                }, option);
                containerArr.push(select);
            }

            if (options.quickLink.length > 0) {
                for (var index in options.quickLink[0]) {
                    quickLink.push(this.generateElement('span', {
                        'data-select': options.quickLink[0][index]
                    }, index));
                }
                var qickLinkDiv = this.generateElement('div', {
                    'class': 'quickLink'
                }, quickLink);
                containerArr.push(qickLinkDiv);
            }

            var svg = this.generateElement('svg', {
                'class': 'timezone-map',
                'viewBox': '0 0 ' + options.width + ' ' + options.height
            }, polygon, true);

            if (containerArr.length > 0) {
                var container = this.generateElement('div', {
                    'class': 'Cbox'
                }, containerArr);
                this.element.append(container);

            }
            this.element.append(svg);

            if (options.showHoverText) {
                var hoverZone = this.generateElement('span', {
                    'class': 'hoverZone',
                });
                this.element.append(hoverZone);
            }

            if (options.defaultCss) {
                this.createCss(options);
            }

            this.bindEvent(options);
        },

        /**
                 * [setValue set value in map]
                 * @param {[type]} value        [attribute value]
                 * @param {[type]} attribute         [attribute name]
                 */
        this.setValue = function (value, attribute) {

            this.element.find('svg polygon').attr('data-selected', 'false');
            var elements = this.element.find('svg polygon[data-' + ((attribute) ? (attribute) : ("country")) + '="' + value + '"]');

            if (elements && elements.length) {
                elements.attr('data-selected', 'true');

                if (this.countryCodes.length > 0)
                {
                    var selectedElements = this.element.find('svg polygon[data-' + ((attribute) ? (attribute) : ("country")) + '="' + this.countryCodes[0] + '"]');
                    selectedElements.attr('data-selected', 'true');
                }

                this.countryCodes.unshift(value);
                this.element.find('select option[value="' + ((attribute) ? (elements.attr('data-timeZone')) : (value)) + '"]').prop('selected', true);
                this.element.find('.quickLink span').removeClass('active');
                var findQuickLink = this.element.find('.quickLink span[data-select="' + value + '"]');
                this.element.find('.quickLink span[data-select="' + value + '"]').addClass('active');
                this.element.find('.quickLink span[data-select="' + elements.attr('data-zonename') + '"]').addClass('active');
                $scope.selectionChanged({ countryCodes: this.countryCodes })
            }
        },
        /**
         * [getValue get selected value array]
         * @return {[type]} [description]
         */
        this.getValue = function () {
            var value = [];
            this.element.find('svg polygon[data-selected="true"]').map(function (index, el) {
                value.push($(el).data());
            });
            return value;
        },
        /**
         * [bindEvent bind all event i.e click,mouseenter,mouseleave,change(select)]
         * @return {[type]} [description]
         */
        this.bindEvent = function() {
            var that = this;
            this.element.on('mouseenter', 'svg polygon', function (e) {
                var d = $(this).data();
                $('.timezone-map polygon[data-country="' + d.country + '"]').attr('class', 'active');
                that.element.find('.hoverZone').text(d.country);
            });
            this.element.on('mouseleave', 'svg polygon', function(e) {
                $('.timezone-map polygon').attr('class', '');
                that.element.find('.hoverZone').text('');
            });
            this.element.on('click', 'svg polygon', function() {
                that.setValue($(this).attr('data-country'));
                that.element.trigger("map:clicked");
            });
            this.element.on('change', 'select', function() {
                that.setValue($(this).val());
                that.element.trigger("map:clicked");
            });
            this.element.on('click', '.quickLink span', function() {
                var selectValue = $(this).data().select
                if (selectValue.search('/') > 0) {
                    that.setValue(selectValue, 'timezone');
                } else {
                    that.setValue(selectValue, 'zonename');
                }
                that.element.trigger("map:clicked");
            });
        },
        /**
         * [generateElement description]
         * @param  {[Jquery Object]}  element     [selector]
         * @param  {[type]}  elementAttr [description]
         * @param  {[javascript Object or text]}  chilled      [If we pass javascript object or  array it will append all chilled and if you pass string it will add string(value) inside element ]
         * @param  {Boolean} isSvg       [If it is svg then it will create svg element]
         * @return {[type]}              [description]
         */
        this.generateElement = function(element, elementAttr, chilled, isSvg) {
            if (isSvg) {
                var elementObject = document.createElementNS('http://www.w3.org/2000/svg', element);
            } else {
                var elementObject = document.createElement(element);
            }
            if (elementAttr) {
                for (var key in elementAttr) {
                    elementObject.setAttribute(key, elementAttr[key]);
                }
            }
            if (chilled) {
                if (chilled instanceof Array) {
                    for (var chilleds in chilled) {
                        elementObject.appendChild(chilled[chilleds]);
                    }
                } else if (typeof chilled == 'string') {
                    elementObject.innerHTML = chilled;
                } else {
                    elementObject.appendChild(chilled);
                }

            }

            return elementObject;
        },
        /**
         * [createCss function will create css dynamically it is insert style attribute in  in head ]
         * @param  {[type]} options [options has mapColor,selectedColor,hoverColor ]
         * @return {[type]}         [description]
         */
        this.createCss = function(options) {
            var style = document.createElement('style');
            style.type = 'text/css';
            style.innerHTML = '.timezone-map polygon[data-selected="true"] {fill: ' + options.selectedColor + '}' +
                '.timezone-map polygon { fill: ' + options.mapColor + ';}' +
                '.timezone-map polygon.active {fill: ' + options.hoverColor + ';}' +
                '.timezone-map polygon.highlighted {fill: ' + options.hightlightedColor + ';}' +
                '.timezone-map polygon:hover { cursor: pointer;}' +
                '.Cbox .quickLink{width: 52%;float: right;padding-bottom: 11px;overflow-x: auto; white-space: nowrap;overflow-y: hidden;}' +
                '.Cbox .quickLink span:hover {color:#FFF;background-color: #496A84;  cursor: pointer;}' +
                '.Cbox select{width: 45%;float: left;height: 27px; padding: 0px 0px 0px 10px;}' +
                '.Cbox .quickLink span.active {color: #FFF; background-color: #496A84;}' +
                '.Cbox .quickLink span{ font-weight: 300; border-radius: 3px; color: #000; background-color: #FFF; border: solid 1px #CCC;margin-left: 10px;' +
                'font-size: 9px;padding: 4px 6px 4px 6px;}';
            document.getElementsByTagName('head')[0].appendChild(style);

        }

        this.timeZoneValue = TimeZoneValueProvider.getAllTimeZones();

        this.DEFAULTS = {
            width: 500,
            height: 250,
            hoverColor: '#5A5A5A',
            hightlightedColor: '#E0FFFF',
            selectedColor: '#496A84',
            mapColor: '#BBB',
            defaultCss: true,
            localStore: true,
            quickLink: [{ }],
            selectBox: false,
            showHoverText: false,
            dayLightSaving: ((typeof moment == "function") ? (true) : (false))
        };
    }]);