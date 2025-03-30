using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace PR6
{
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
            TextBoxPhone.TextChanged += PhoneNumberTextChanged;
        }

        private void PhoneNumberTextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            string text = textBox.Text.Replace("+7", "").Replace("(", "").Replace(")", "").Replace("-", "").Replace(" ", "");

            if (text.Length > 0)
            {
                string formatted = "+7 " + (text.Length > 0 ? "(" + text.Substring(0, Math.Min(3, text.Length)) : "") +
                                 (text.Length > 3 ? ") " + text.Substring(3, Math.Min(3, text.Length - 3)) : "") +
                                 (text.Length > 6 ? "-" + text.Substring(6, Math.Min(2, text.Length - 6)) : "") +
                                 (text.Length > 8 ? "-" + text.Substring(8) : "");

                textBox.Text = formatted;
                textBox.CaretIndex = formatted.Length;
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new AuthPage());
        }

        private void BtnRegister_Click(object sender, RoutedEventArgs e)
        {
            Register(TextBoxFullName.Text, TextBoxEmail.Text, PasswordBox.Password,
                   RepeatPasswordBox.Password, (CmbRole.SelectedItem as ComboBoxItem)?.Content.ToString(),
                   TextBoxPhone.Text, DatePickerBirth.SelectedDate,
                   (CmbCity.SelectedItem as ComboBoxItem)?.Content.ToString());
        }

        public bool Register(string fullName, string email, string password, string repeatPassword, string role, string phone, DateTime? birthDate, string city)
        {
            if (string.IsNullOrWhiteSpace(fullName) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(repeatPassword) ||
                string.IsNullOrWhiteSpace(role) ||
                string.IsNullOrWhiteSpace(phone))
            {
                MessageBox.Show("Заполните все обязательные поля!");
                return false;
            }

            if (password != repeatPassword)
            {
                MessageBox.Show("Пароли не совпадают!");
                return false;
            }

            if (password.Length < 8)
            {
                MessageBox.Show("Пароль должен содержать минимум 8 символов!");
                return false;
            }

            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                MessageBox.Show("Введите корректный email адрес!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            try
            {
                using (var db = new PR6Entities1())
                {
                    if (db.Users.Any(u => u.Login == email))
                    {
                        MessageBox.Show("Пользователь с таким email уже существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return false;
                    }

                    var newUser = new Users
                    {
                        FIO = fullName,
                        Login = email,
                        Password = password,
                        Role = role,
                        PhoneNumber = phone,
                        BirthDate = birthDate,
                        City = city
                    };

                    db.Users.Add(newUser);
                    db.SaveChanges();

                    MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при регистрации: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }
    }
}