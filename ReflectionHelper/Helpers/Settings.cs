using System;
using System.Linq;
using System.Reflection;
#if RHELPER
using ReflectionFramework.Attributes;
using ReflectionFramework.Internal;

namespace ReflectionFramework {
#else
using DevExpress.Xpf.Core.ReflectionExtensions.Internal;
using DevExpress.Xpf.Core.ReflectionExtensions.Attributes;

namespace DevExpress.Xpf.Core.Internal {
#endif
    internal abstract class BaseReflectionHelperInterfaceWrapperSetting {
        readonly BaseReflectionHelperInterfaceWrapperGenerator reflectionHelperInterfaceWrapperGenerator;

        public BaseReflectionHelperInterfaceWrapperSetting(BaseReflectionHelperInterfaceWrapperGenerator reflectionHelperInterfaceWrapperGenerator) {
            this.reflectionHelperInterfaceWrapperGenerator = reflectionHelperInterfaceWrapperGenerator;
        }

        TAttribute GetAttribute<TAttribute>() {
            return reflectionHelperInterfaceWrapperGenerator.tWrapper.GetCustomAttributes(typeof(TAttribute), true).OfType<TAttribute>().FirstOrDefault();
        }
        TAttribute GetAttribute<TAttribute>(MemberInfo memberInfo) {
            if (memberInfo == null)
                return default(TAttribute);
            return memberInfo.GetCustomAttributes(typeof(TAttribute), true).OfType<TAttribute>().FirstOrDefault();
        }

        internal virtual BindingFlags GetBindingFlags(MemberInfo primaryMethodInfo, MemberInfo secondaryMemberInfo) {            
            var attr = GetAttribute<BindingFlagsAttribute>(primaryMethodInfo) ?? GetAttribute<BindingFlagsAttribute>(secondaryMemberInfo) ?? GetAttribute<BindingFlagsAttribute>();
            if (attr != null)
                return attr.Flags;
            if (GetAttribute<InterfaceMemberAttribute>(primaryMethodInfo) != null)
                return BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;
            return reflectionHelperInterfaceWrapperGenerator.defaultFlags;
        }

        internal virtual bool GetIsInterface(string name, MemberInfo memberInfo) {
            return GetAttribute<InterfaceMemberAttribute>(memberInfo)!=null;
        }
        internal virtual string GetName(string defaultName, MemberInfo memberInfo) {
            var attr = GetAttribute<NameAttribute>(memberInfo);
            if (attr != null)
                return attr.Name;
            
            return defaultName;
        }

        internal virtual bool FieldAccessor(MemberInfo memberInfo) {
            var attr = GetAttribute<FieldAccessorAttribute>(memberInfo);
            if (attr != null)
                return true;
            return false;
        }

        internal virtual Delegate GetFallback(MemberInfoKind kind) { return null; }

        internal virtual ReflectionHelperFallbackMode GetFallbackMode(MethodInfo wrapperMethodInfo, MemberInfo baseInfo, Type tWrapper) {
            var attribute = GetAttribute<FallbackModeAttribute>(wrapperMethodInfo) ??
                            GetAttribute<FallbackModeAttribute>(baseInfo) ??
                            tWrapper.GetCustomAttributes(typeof(FallbackModeAttribute), true).OfType<FallbackModeAttribute>().FirstOrDefault();
            if (attribute == null)
                return ReflectionHelperFallbackMode.Default;
            return attribute.Mode;
        }

        public abstract int ComputeKey();        
    }

    internal class ReflectionHelperInterfaceWrapperSetting : BaseReflectionHelperInterfaceWrapperSetting {
        public ReflectionHelperInterfaceWrapperSetting(BaseReflectionHelperInterfaceWrapperGenerator reflectionHelperInterfaceWrapperGenerator)
            : base(reflectionHelperInterfaceWrapperGenerator) {}

        public BindingFlags? BindingFlags { get; set; }
        public string Name { get; set; }
        public bool IsField { get; set; }
        public Delegate FallbackAction { get; set; }
        public Delegate GetterFallbackAction { get; set; }
        public Delegate SetterFallbackAction { get; set; }
        public string InterfaceName { get; set; }
        public ReflectionHelperFallbackMode FallbackMode { get; set; }

        internal override BindingFlags GetBindingFlags(MemberInfo primaryMethodInfo, MemberInfo secondaryMemberInfo) {
            return BindingFlags ?? base.GetBindingFlags(primaryMethodInfo, secondaryMemberInfo);
        }

        internal override string GetName(string defaultName, MemberInfo memberInfo) {
            string prefix = "";
            if (!string.IsNullOrEmpty(InterfaceName)) {
                prefix = InterfaceName + ".";
            }
            return prefix + (Name ?? base.GetName(defaultName, memberInfo));
        }

        internal override bool FieldAccessor(MemberInfo memberInfo) {
            return IsField;
        }

        internal override Delegate GetFallback(MemberInfoKind infoKind) {
            Delegate result = null;
            switch (infoKind) {
                case MemberInfoKind.Method:
                    result = FallbackAction;
                    break;
                case MemberInfoKind.PropertyGetter:
                    result = GetterFallbackAction;
                    break;
                case MemberInfoKind.PropertySetter:
                    result = SetterFallbackAction;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("infoKind", infoKind, null);
            }
            return result ?? base.GetFallback(infoKind);
        }
        internal override ReflectionHelperFallbackMode GetFallbackMode(MethodInfo wrapperMethodInfo, MemberInfo baseInfo, Type tWrapper) { return FallbackMode == ReflectionHelperFallbackMode.Default ? base.GetFallbackMode(wrapperMethodInfo, baseInfo, tWrapper) : FallbackMode; }

        public override int ComputeKey() {
            unchecked {
                var hashCode = BindingFlags.GetHashCode();
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ IsField.GetHashCode();
                hashCode = (hashCode * 397) ^ (FallbackAction != null ? FallbackAction.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (GetterFallbackAction != null ? GetterFallbackAction.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (SetterFallbackAction != null ? SetterFallbackAction.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (InterfaceName != null ? InterfaceName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (FallbackMode != null ? FallbackMode.GetHashCode() : 0);
                return hashCode;
            }
        }
    }

    internal class NullReflectionHelperInterfaceWrapperSetting : BaseReflectionHelperInterfaceWrapperSetting {
        public NullReflectionHelperInterfaceWrapperSetting(BaseReflectionHelperInterfaceWrapperGenerator reflectionHelperInterfaceWrapperGenerator) : base(reflectionHelperInterfaceWrapperGenerator) {}
        public override int ComputeKey() {
            return 0;
        }
    }
}