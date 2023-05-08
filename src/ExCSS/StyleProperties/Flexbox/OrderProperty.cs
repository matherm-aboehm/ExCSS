﻿namespace ExCSS
{
    internal sealed class OrderProperty : Property
    {
        private static readonly IValueConverter StyleConverter = Converters.IntegerConverter.OrDefault(0);

        internal OrderProperty()
            : base(PropertyNames.Order)
        { }

        internal override IValueConverter Converter => StyleConverter;
    }
}
