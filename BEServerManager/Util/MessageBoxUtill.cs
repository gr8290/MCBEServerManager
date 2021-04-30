using System.Windows;

namespace BEServerManager.Util
{
    class MessageBoxUtil
    {
        public static void WarningMessageBoxShow(string msg)
        {
            MessageBox.Show(msg, "警告", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        public static bool QuestionMessageBoxShow(string msg)
        {
            return MessageBox.Show(msg, "質問", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
        }

        public static void AffirmationMessageBoxShow(string msg)
        {
            MessageBox.Show(msg, "確認", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void ExceptionMessageBoxShow(string msg)
        {
            MessageBox.Show(msg, "例外", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
