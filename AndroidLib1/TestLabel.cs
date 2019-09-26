using System;
using System.ComponentModel;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Widget;

namespace AndroidLib1
{
    [Register("androidlib1.TestLabel")]
    [DesignTimeVisible(true)]
    public class TestLabel : TextView
    {
        private int visibleItemCount;

        [Browsable(true)]
        public int VisibleItemCount
        {
            get => visibleItemCount;
            set
            {
                if (value < 2)
                    throw new ArithmeticException("VisibleItemCount must be greater than 2");
                visibleItemCount = value;
            }
        }

        [Preserve(Conditional = true)]
        protected TestLabel(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        [Preserve(Conditional = true)]
        public TestLabel(Context context) : base(context)
        {
            Init(context);
        }

        [Preserve(Conditional = true)]
        public TestLabel(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            Init(context, attrs);
        }

        [Preserve(Conditional = true)]
        public TestLabel(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            Init(context, attrs, defStyleAttr);
        }

        [Preserve(Conditional = true)]
        public TestLabel(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
        {
            Init(context, attrs, defStyleAttr, defStyleRes);
        }

        private void Init(Context context, IAttributeSet attrs = null, int defStyleAttr = 0, int defStyleRes = 0)
        {
            if (defStyleRes == 0)
                defStyleRes = Resource.Style.TestLabelStyle;

            using (var a = attrs != null ?
                 context.ObtainStyledAttributes(attrs, Resource.Styleable.TestLabel, defStyleAttr, defStyleRes)
                 : context.ObtainStyledAttributes(defStyleRes, Resource.Styleable.TestLabel))
            {
                VisibleItemCount = a.GetInt(Resource.Styleable.TestLabel_visibleItemCount, 0); //Should never be 0 by default
                Text = $"Value: {VisibleItemCount}";
                a.Recycle();
            }
        }
    }
}
