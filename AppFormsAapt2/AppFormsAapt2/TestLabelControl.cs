using Xamarin.Forms;

namespace AppFormsAapt2
{
    public class TestLabelControl : View
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(TestLabelControl), "default value");

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }
    }
}
