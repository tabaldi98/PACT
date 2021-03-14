using System.Windows.Forms;

namespace app.Tabaldi.PACT.App.Commom
{
    public static class FormExtensions
    {
        public static bool IsNullOrEmpty(this TextBox textBox)
        {
            return string.IsNullOrEmpty(textBox.Text);
        }

        public static bool IsNullOrEmpty(this RichTextBox textBox)
        {
            return string.IsNullOrEmpty(textBox.Text);
        }

        public static void SetLoading(this Form form, bool loading)
        {
            form.Enabled = !loading;
            Cursor.Current = !loading ? Cursors.WaitCursor : Cursors.Default;
        }

        public static void SetLoading(this UserControl form, bool loading)
        {
            form.Enabled = !loading;
            Cursor.Current = !loading ? Cursors.WaitCursor : Cursors.Default;
        }

        public static void SetIsLoading(this UserControl form)
        {
            form.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
        }

        public static void SetNoLoading(this UserControl form)
        {
            form.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        public static void SetIsLoading(this Form form)
        {
            form.Enabled = false;
            Cursor.Current = Cursors.WaitCursor;
        }

        public static void SetNoLoading(this Form form)
        {
            form.Enabled = true;
            Cursor.Current = Cursors.Default;
        }
    }
}
