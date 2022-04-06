module.exports =
/******/ (function(modules) { // webpackBootstrap
/******/ 	// The module cache
/******/ 	var installedModules = {};

/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {

/******/ 		// Check if module is in cache
/******/ 		if(installedModules[moduleId])
/******/ 			return installedModules[moduleId].exports;

/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = installedModules[moduleId] = {
/******/ 			exports: {},
/******/ 			id: moduleId,
/******/ 			loaded: false
/******/ 		};

/******/ 		// Execute the module function
/******/ 		modules[moduleId].call(module.exports, module, module.exports, __webpack_require__);

/******/ 		// Flag the module as loaded
/******/ 		module.loaded = true;

/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}


/******/ 	// expose the modules object (__webpack_modules__)
/******/ 	__webpack_require__.m = modules;

/******/ 	// expose the module cache
/******/ 	__webpack_require__.c = installedModules;

/******/ 	// __webpack_public_path__
/******/ 	__webpack_require__.p = "";

/******/ 	// Load entry module and return exports
/******/ 	return __webpack_require__(0);
/******/ })
/************************************************************************/
/******/ ({

/***/ 0:
/***/ (function(module, exports, __webpack_require__) {

	module.exports = __webpack_require__(1483);


/***/ }),

/***/ 3:
/***/ (function(module, exports) {

	module.exports = function() { throw new Error("define cannot be used indirect"); };


/***/ }),

/***/ 1057:
/***/ (function(module, exports) {

	module.exports = require("./kendo.core");

/***/ }),

/***/ 1325:
/***/ (function(module, exports) {

	module.exports = require("./kendo.floatinglabel");

/***/ }),

/***/ 1483:
/***/ (function(module, exports, __webpack_require__) {

	var __WEBPACK_AMD_DEFINE_FACTORY__, __WEBPACK_AMD_DEFINE_ARRAY__, __WEBPACK_AMD_DEFINE_RESULT__;(function (f, define) {
	    !(__WEBPACK_AMD_DEFINE_ARRAY__ = [__webpack_require__(1057), __webpack_require__(1325)], __WEBPACK_AMD_DEFINE_FACTORY__ = (f), __WEBPACK_AMD_DEFINE_RESULT__ = (typeof __WEBPACK_AMD_DEFINE_FACTORY__ === 'function' ? (__WEBPACK_AMD_DEFINE_FACTORY__.apply(exports, __WEBPACK_AMD_DEFINE_ARRAY__)) : __WEBPACK_AMD_DEFINE_FACTORY__), __WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__));
	})(function () {

	var __meta__ = {// jshint ignore:line
	    id: "textbox",
	    name: "TextBox",
	    category: "web",
	    description: "The TextBox widget enables you to style and provide a floating label functionality to input elements",
	    depends: ["core", "floatinglabel"]
	};

	(function ($, undefined) {
	    var kendo = window.kendo,
	        Widget = kendo.ui.Widget,
	        ui = kendo.ui,
	        isPlainObject = $.isPlainObject,
	        NS = ".kendoTextBox",
	        CHANGE = "change",
	        DISABLED = "disabled",
	        READONLY = "readonly",
	        INPUT = "k-input-inner",
	        FOCUSED = "k-focus",
	        LABELCLASSES = "k-label k-input-label",
	        STATEDISABLED = "k-disabled",
	        NOCLICKCLASS = "k-no-click",
	        ARIA_DISABLED = "aria-disabled",
	        proxy = $.proxy;

	    var TextBox = Widget.extend({
	        init: function (element, options) {
	            var that = this;

	            Widget.fn.init.call(that, element, options);
	            options = $.extend(true, {}, options);

	            that.options.value = options.value || that.element.val();
	            that.options.readonly = options.readonly !== undefined ? options.readonly : Boolean(that.element.attr("readonly"));
	            that.options.enable = options.enable !== undefined ? options.enable : !(Boolean(that.element.attr("disabled")));
	            that.options.placeholder = options.placeholder || that.element.attr("placeholder");

	            that.value(that.options.value);
	            that._wrapper();
	            that._label();
	            that._editable({
	                readonly: that.options.readonly,
	                disable: !(that.options.enable)
	            });

	            that.element
	                .addClass(INPUT)
	                .attr("placeholder", that.options.placeholder)
	                .attr("autocomplete", "off");

	            kendo.notify(that);
	            that._applyCssClasses();
	        },

	        events: [
	            CHANGE
	        ],

	        options: {
	            name: 'TextBox',
	            value: '',
	            readonly: false,
	            enable: true,
	            placeholder: '',
	            label: null,
	            rounded: "medium",
	            size: "medium",
	            fillMode: "solid"
	        },

	        value: function(value) {
	            var that = this;

	            if (value === undefined) {
	                return that._value;
	            }

	            that._value = value;
	            that.element.val(value);
	        },

	        readonly: function(readonly) {
	            var that = this;

	            this._editable({
	                readonly: readonly === undefined ? true : readonly,
	                disable: false
	            });

	            if (that.floatingLabel) {
	                that.floatingLabel.readonly(readonly === undefined ? true : readonly);
	            }
	        },

	        enable: function(enable) {
	            var that = this;

	            this._editable({
	                readonly: false,
	                disable: !(enable = enable === undefined ? true : enable)
	            });

	            if (that.floatingLabel) {
	                that.floatingLabel.enable(enable = enable === undefined ? true : enable);
	            }
	        },

	        focus: function() {
	            var that = this;

	            that.element[0].focus();
	        },

	        destroy: function() {
	            var that = this;

	            if (that.floatingLabel) {
	                that.floatingLabel.destroy();
	            }

	            that.element.off(NS);
	            that.element[0].style.width = "";
	            that.element.removeClass(INPUT);
	            Widget.fn.destroy.call(that);
	        },

	        setOptions: function(options) {
	            this.destroy();

	            this.element.unwrap();
	            if (this._floatingLabelContainer) {
	                this._inputLabel.remove();
	                this._inputLabel = null;
	                this.element.unwrap();
	                this._floatingLabelContainer = null;
	            }

	            kendo.deepExtend(this.options, options);
	            this.init(this.element, this.options);
	        },

	        _editable: function(options) {
	            var that = this;
	            var element = that.element;
	            var wrapper = that.wrapper;
	            var disable = options.disable;
	            var readonly = options.readonly;

	            element.off(NS);

	            if (!readonly && !disable) {
	                element.prop(DISABLED, false)
	                       .prop(READONLY, false)
	                       .attr(ARIA_DISABLED, false);

	                wrapper.removeClass(STATEDISABLED)
	                        .removeClass(NOCLICKCLASS);

	                element.on("focusin" + NS, proxy(that._focusin, that));
	                element.on("focusout" + NS, proxy(that._focusout, that));
	                element.on("change" + NS, proxy(that._change, that));
	            } else {
	                element.attr(DISABLED, disable)
	                       .attr(READONLY, readonly)
	                       .attr(ARIA_DISABLED, disable);

	                wrapper.toggleClass(STATEDISABLED, disable)
	                        .toggleClass(NOCLICKCLASS, readonly);
	            }
	        },

	        _label: function() {
	            var that = this;
	            var element = that.element;
	            var options = that.options;
	            var id = element.attr("id");
	            var floating;
	            var labelText;

	            if (options.label !== null) {
	                floating = isPlainObject(options.label) ? options.label.floating : false;
	                labelText = isPlainObject(options.label) ? options.label.content : options.label;

	                if (floating) {
	                    that._floatingLabelContainer = that.wrapper.wrap("<span></span>").parent();
	                    that.floatingLabel = new kendo.ui.FloatingLabel(that._floatingLabelContainer, { widget: that });
	                }

	                if (kendo.isFunction(labelText)) {
	                    labelText = labelText.call(that);
	                }

	                if (!labelText) {
	                    labelText = "";
	                }

	                if (!id) {
	                    id = options.name + "_" + kendo.guid();
	                    element.attr("id", id);
	                }

	                that._inputLabel = $("<label class='" + LABELCLASSES + "' for='" + id + "'>" + labelText + "</label>'").insertBefore(that.wrapper);
	            }
	        },

	        _focusin: function() {
	            var that = this;

	            that.wrapper.addClass(FOCUSED);
	        },

	        _focusout: function() {
	            var that = this;

	            that.wrapper.removeClass(FOCUSED);
	        },

	        _change: function(e) {
	            var that = this;
	            var newValue = that.element.val();

	            that._value = newValue;

	            that.trigger(CHANGE, {value: newValue, originalEvent: e});
	        },

	        _wrapper: function () {
	            var that = this;
	            var element = that.element;
	            var DOMElement = element[0];
	            var wrapper;

	            wrapper = element.wrap("<span class='k-input k-textbox'></span>").parent();
	            wrapper[0].style.cssText = DOMElement.style.cssText;
	            DOMElement.style.width = "100%";
	            that.wrapper = wrapper.addClass(DOMElement.className).removeClass('input-validation-error');
	        }
	    });

	    kendo.cssProperties.registerPrefix("TextBox", "k-input-");

	    kendo.cssProperties.registerValues("TextBox", [{
	        prop: "rounded",
	        values: kendo.cssProperties.roundedValues.concat([['full', 'full']])
	    }]);

	    ui.plugin(TextBox);
	})(window.kendo.jQuery);

	return window.kendo;

	}, __webpack_require__(3));


/***/ })

/******/ });