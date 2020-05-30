var VALIDACIONESTP = VALIDACIONESTP || {};

VALIDACIONESTP.validacionGnrl = {
    rangett: function () {
        jQuery.validator.addMethod('rangeifdoblebooltt', function (value, element, params) {
            var acc = $('#' + params.valuebool).prop('checked');
            var acc2 = $('#' + params.valuebool2).prop('checked');

            return !acc || !acc2 || (acc && acc2 && value >= params.min && value <= params.max);
        });

        jQuery.validator.unobtrusive.adapters.add('rangeifdoblebooltt', ['valuebool', 'valuebool2', 'min', 'max'],
            function (options) {
                options.rules['rangeifdoblebooltt'] = { valuebool: options.params.valuebool, valuebool2: options.params.valuebool2, min: parseInt(options.params.min), max: parseInt(options.params.max) };
                options.messages['rangeifdoblebooltt'] = options.message;
            });
    },

    range: function () {
        jQuery.validator.addMethod('rangeifBool', function (value, element, params) {
            var acc = $('#' + params.valuebool).prop('checked');

            return acc != params.opc || (acc == params.opc && value >= params.min && value <= params.max);
        });

        jQuery.validator.unobtrusive.adapters.add('rangeifBool', ['valuebool', 'min', 'max', 'opc'],
            function (options) {
                options.rules['rangeifBool'] = { valuebool: options.params.valuebool, min: parseInt(options.params.min), max: parseInt(options.params.max), opc: options.params.opc == "True" };
                options.messages['rangeifBool'] = options.message;
            });
    },

    requiredif: function () {
        jQuery.validator.addMethod('requiredifbool', function (value, element, params) {
            var acc = $('#' + params.valuebool).prop('checked');

            return !acc || (acc && value !== "" && value != null);
        });

        jQuery.validator.unobtrusive.adapters.add('requiredifbool', ['valuebool'],
            function (options) {
                options.rules['requiredifbool'] = { valuebool: options.params.valuebool };
                options.messages['requiredifbool'] = options.message;
            });
    }
};