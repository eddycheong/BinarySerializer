﻿using BinarySerialization;

namespace Iso9660
{
    class SectorByteConverter : IValueConverter
    {
        public object Convert(object value, object converterParameter, BinarySerializationContext ctx)
        {
            var sector = (uint) value;
            var iso = ctx.FindAncestor<Iso9660>();
            var sectorSize = iso.PrimaryVolumeDescriptor.SectorSize;
            return sector*sectorSize;
        }

        public object ConvertBack(object value, object converterParameter, BinarySerializationContext ctx)
        {
            throw new System.NotImplementedException();
        }
    }
}
