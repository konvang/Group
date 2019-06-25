using System.Windows;
using System.Windows.Controls;

namespace SaftStopSystem
{
    /// <summary>
    /// The class which is used to help control a password box.
    /// </summary>
    public static class PasswordBoxAssistant
    {
        /// <summary>
        /// Password to bound to an account.
        /// </summary>
        public static readonly DependencyProperty BoundPassword =
        DependencyProperty.RegisterAttached("BoundPassword", typeof(string), typeof(PasswordBoxAssistant), new PropertyMetadata(string.Empty, OnBoundPasswordChanged));

        /// <summary>
        /// A password to bind.
        /// </summary>
        public static readonly DependencyProperty BindPassword = DependencyProperty.RegisterAttached(
        "BindPassword", typeof(bool), typeof(PasswordBoxAssistant), new PropertyMetadata(false, OnBindPasswordChanged));

        /// <summary>
        /// The password to update.
        /// </summary>
        private static readonly DependencyProperty UpdatingPassword =
        DependencyProperty.RegisterAttached("UpdatingPassword", typeof(bool), typeof(PasswordBoxAssistant), new PropertyMetadata(false));

        /// <summary>
        /// The password to bind with another account.
        /// </summary>
        /// <param name="dp">The object dependency.</param>
        /// <param name="value">The value of the password.</param>
        public static void SetBindPassword(DependencyObject dp, bool value)
        {
            dp.SetValue(BindPassword, value);
        }

        /// <summary>
        /// The password to bind.
        /// </summary>
        /// <param name="dp">Object to return.</param>
        /// <returns>The value of the password.</returns>
        public static bool GetBindPassword(DependencyObject dp)
        {
            return (bool)dp.GetValue(BindPassword);
        }

        /// <summary>
        /// The password to be bounded.
        /// </summary>
        /// <param name="dp">The object of the password.</param>
        /// <returns>The value to get.</returns>
        public static string GetBoundPassword(DependencyObject dp)
        {
            return (string)dp.GetValue(BoundPassword);
        }

        /// <summary>
        /// Password to set for bounds.
        /// </summary>
        /// <param name="dp">The object to return.</param>
        /// <param name="value">The value to receive.</param>
        public static void SetBoundPassword(DependencyObject dp, string value)
        {
            dp.SetValue(BoundPassword, value);
        }

        /// <summary>
        /// The password to set for bounds.
        /// </summary>
        /// <param name="d">The object of the password.</param>
        /// <param name="e">The property change of the password.</param>
        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox box = d as PasswordBox;

            // only handle this event when the property is attached to a PasswordBox
            // and when the BindPassword attached property has been set to true
            if (d == null || !GetBindPassword(d))
            {
                return;
            }

            // avoid recursive updating by ignoring the box's changed event
            box.PasswordChanged -= HandlePasswordChanged;

            string newPassword = (string)e.NewValue;

            if (!GetUpdatingPassword(box))
            {
                box.Password = newPassword;
            }

            box.PasswordChanged += HandlePasswordChanged;
        }

        /// <summary>
        /// Password to be changed.
        /// </summary>
        /// <param name="dp">The dependency of the object.</param>
        /// <param name="e">The property change of the password.</param>
        private static void OnBindPasswordChanged(DependencyObject dp, DependencyPropertyChangedEventArgs e)
        {
            //// when the BindPassword attached property is set on a PasswordBox,
            //// start listening to its PasswordChanged event

            PasswordBox box = dp as PasswordBox;

            if (box == null)
            {
                return;
            }

            bool wasBound = (bool)e.OldValue;
            bool needToBind = (bool)e.NewValue;

            if (wasBound)
            {
                box.PasswordChanged -= HandlePasswordChanged;
            }

            if (needToBind)
            {
                box.PasswordChanged += HandlePasswordChanged;
            }
        }

        /// <summary>
        /// Handler for when passwords are changed.
        /// </summary>
        /// <param name="sender">The object that is calling this method.</param>
        /// <param name="e">Event args.</param>
        private static void HandlePasswordChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox box = sender as PasswordBox;

            //// set a flag to indicate that we're updating the password
            SetUpdatingPassword(box, true);
            //// push the new password into the BoundPassword property
            SetBoundPassword(box, box.Password);
            SetUpdatingPassword(box, false);
        }

        /// <summary>
        /// The password to get when updating.
        /// </summary>
        /// <param name="dp">The object of the password.</param>
        /// <returns>The value the password.</returns>
        private static bool GetUpdatingPassword(DependencyObject dp)
        {
            return (bool)dp.GetValue(UpdatingPassword);
        }

        /// <summary>
        /// The password to set.
        /// </summary>
        /// <param name="dp">The object of the password.</param>
        /// <param name="value">The value of the password.</param>
        private static void SetUpdatingPassword(DependencyObject dp, bool value)
        {
            dp.SetValue(UpdatingPassword, value);
        }
    }
}
