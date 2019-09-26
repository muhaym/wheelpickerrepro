using System;
using System.ComponentModel;
using Android.Content;
using Android.Views;
using AndroidLib1;
using AppFormsAapt2;
using AppFormsAapt2.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(TestLabelControl), typeof(TestLabelRenderer))]

namespace AppFormsAapt2.Droid
{
    class TestLabelRenderer : TestLabel, IVisualElementRenderer
    {
        private TestLabelControl element;
        VisualElementTracker visualElementTracker;
        private int? defaultLabelFor;

        public event EventHandler<VisualElementChangedEventArgs> ElementChanged;
        public event EventHandler<PropertyChangedEventArgs> ElementPropertyChanged;

        VisualElement IVisualElementRenderer.Element => element;
        VisualElementTracker IVisualElementRenderer.Tracker => visualElementTracker;
        public Android.Views.View View => this;
        public ViewGroup ViewGroup => null;

        public TestLabelRenderer(Context context) : base(context)
        {
        }

        //Must be called before Forms.Init() so the custom renderer is discovered
        //https://bugzilla.xamarin.com/show_bug.cgi?id=30580               
        public static void InitializeForms()
        {
        }

        private void SetElement(TestLabelControl value)
        {
            if (element == value)
                return;
            var oldElement = element;
            element = value;
            OnElementChanged(new ElementChangedEventArgs<TestLabelControl>(oldElement, element));
            //element?.SendViewInitialized(this); //Internal in Forms!
        }

        private void OnElementChanged(ElementChangedEventArgs<TestLabelControl> e)
        {
            ElementChanged?.Invoke(this, new VisualElementChangedEventArgs(e.OldElement, e.NewElement));

            if (e.OldElement != null)
            {
                e.OldElement.PropertyChanged -= OnElementPropertyChanged;
            }

            if (e.NewElement != null)
            {
                this.EnsureId();
                if (visualElementTracker == null)
                    visualElementTracker = new VisualElementTracker(this);
                CreateNative();
                e.NewElement.PropertyChanged += OnElementPropertyChanged;
            }
        }

        private void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            ElementPropertyChanged?.Invoke(this, e);

            if (e.PropertyName == TestLabelControl.TextProperty.PropertyName)
            {
                Text = element.Text;
            }
        }

        private void CreateNative()
        {
        }


        #region Fast renderer
        SizeRequest IVisualElementRenderer.GetDesiredSize(int widthConstraint, int heightConstraint)
        {
            Measure(widthConstraint, heightConstraint);
            return new SizeRequest(new Size(MeasuredWidth, MeasuredHeight), new Size(MinimumWidth, MinimumHeight));
        }

        void IVisualElementRenderer.SetElement(VisualElement el)
        {
            SetElement(el as TestLabelControl ?? throw new ArgumentException($"Element must be of type {nameof(TestLabelControl)}"));
            if (!string.IsNullOrEmpty(element.AutomationId))
                ContentDescription = element.AutomationId;
        }

        void IVisualElementRenderer.SetLabelFor(int? id)
        {
            if (defaultLabelFor == null)
                defaultLabelFor = LabelFor;
            LabelFor = (int)(id ?? defaultLabelFor);
        }

        void IVisualElementRenderer.UpdateLayout()
        {
            visualElementTracker?.UpdateLayout();
        }
        #endregion
    }
}
