using System;
using NasgaMe.Utility;
using PostSharp.Aspects;

namespace NasgaMe.Aspects
{
    [Serializable]
    public class CacheAttribute : OnMethodBoundaryAspect
    {
        public override void OnEntry(MethodExecutionArgs args)
        {
            string key = string.Format("{0}_{1}", args.Method.Name, Cache.GenerateKey(args.Arguments));
            object value = Cache.Get(key);

            if (value == null) args.MethodExecutionTag = key;
            else
            {
                args.ReturnValue = value;
                args.FlowBehavior = FlowBehavior.Return;
            }
        }

        public override void OnSuccess(MethodExecutionArgs args)
        {
            string key = args.MethodExecutionTag.ToString();
            Cache.Set(key, args.ReturnValue);
        }
    }
}