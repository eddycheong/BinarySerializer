using System;
using System.Collections;
using System.IO;
using System.Reflection;

namespace BinarySerialization.Graph.TypeGraph
{
    internal abstract class ContainerTypeNode : TypeNode
    {
        protected ContainerTypeNode(TypeNode parent, Type type)
            : base(parent, type)
        {
        }

        protected ContainerTypeNode(TypeNode parent, MemberInfo memberInfo) : base(parent, memberInfo)
        {
        }

        protected TypeNode GenerateChild(Type type)
        {
            try
            {
                ThrowOnBadType(type);

                var nodeType = GetNodeType(type);

                var child = (TypeNode)Activator.CreateInstance(nodeType, this, type);
                return child;
            }
            catch (Exception exception)
            {
                var message = string.Format("There was an error reflecting graphType '{0}'", type);
                throw new InvalidOperationException(message, exception);
            }
        }

        protected TypeNode GenerateChild(MemberInfo memberInfo)
        {
            var memberType = GetMemberType(memberInfo);

            try
            {
                ThrowOnBadType(memberType);

                var nodeType = GetNodeType(memberType);

                return (TypeNode) Activator.CreateInstance(nodeType, this, memberInfo);
            }
            catch (Exception exception)
            {
                var message = string.Format("There was an error reflecting member '{0}'", memberInfo.Name);
                throw new InvalidOperationException(message, exception);
            }
        }

// ReSharper disable UnusedParameter.Local
        private static void ThrowOnBadType(Type type)
        {
            if (typeof(IDictionary).IsAssignableFrom(type))
                throw new InvalidOperationException("Cannot serialize objects that implement IDictionary.");
        }
// ReSharper restore UnusedParameter.Local

        private static Type GetNodeType(Type type)
        {
            var nullableType = Nullable.GetUnderlyingType(type);

            var effectiveType = nullableType ?? type;

            if (effectiveType.IsEnum)
                return typeof(EnumTypeNode);
            if (effectiveType.IsPrimitive || effectiveType == typeof(string) || effectiveType == typeof(byte[]))
                return typeof (ValueTypeNode);

            if (type.IsArray)
                return typeof(ArrayTypeNode);
            if (typeof(IList).IsAssignableFrom(type))
                return typeof(ListTypeNode);
            if (typeof(Stream).IsAssignableFrom(type))
                return typeof(StreamTypeNode);
            if (typeof(IBinarySerializable).IsAssignableFrom(type))
                return typeof(CustomTypeNode);
            if (type == typeof(object))
                return typeof (UnknownTypeNode);
            return typeof (ObjectTypeNode);
        }

        protected static Type GetMemberType(MemberInfo memberInfo)
        {
            var propertyInfo = memberInfo as PropertyInfo;
            var fieldInfo = memberInfo as FieldInfo;
            
            if (propertyInfo != null)
            {
                return propertyInfo.PropertyType;
            }

            if (fieldInfo != null)
            {
                return fieldInfo.FieldType;
            }

            throw new NotSupportedException(string.Format("{0} not supported", memberInfo.GetType().Name));
        }

    }
}