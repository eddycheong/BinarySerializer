﻿using System;

namespace BinarySerialization.TypeGraph
{
    internal class IntegerBinding : Binding
    {
        //private readonly ulong? _constValue;
 
        public IntegerBinding(Node targetNode, BindingInfo bindingInfo) 
            : base(targetNode, bindingInfo)
        {
            //if (string.IsNullOrEmpty(attribute.Path))
            //{
            //    _constValue = (ulong) attribute.GetConstValue();
            //}
        }

        //public bool IsConst
        //{
        //    get { return _constValue.HasValue; }
        //}

        //public ulong Value
        //{
        //    get
        //    {
        //        return _constValue.HasValue ? _constValue.Value : System.Convert.ToUInt64(GetValue());
        //    }
        //}

        //public ulong BoundValue
        //{
        //    get
        //    {
        //        return _constValue.HasValue ? _constValue.Value : System.Convert.ToUInt64(GetBoundValue());
        //    }
        //}
    }
}
