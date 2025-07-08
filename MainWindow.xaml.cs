using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Windows.Media.Animation;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<MenuItem> items = new();
        private decimal tipAmount = 0;
        public MainWindow()
        {
            InitializeComponent();
            itemsDataGrid.ItemsSource = items;
            UpdateTotals();
        }
        private void AddItem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ResetTextBoxStyle(descriptionTextBox);
                ResetTextBoxStyle(priceTextBox);

                string description = descriptionTextBox.Text.Trim();
                string priceText = priceTextBox.Text.Trim().Replace(',', '.');

                // Валидации
                if (description.Length < 3 || description.Length > 20)
                {
                    MarkTextBoxInvalid(descriptionTextBox);
                    MessageBox.Show("Опис має бути від 3 до 20 символів");
                    return;
                }

                if (priceText.Length > 30)
                {
                    MarkTextBoxInvalid(priceTextBox);
                    MessageBox.Show("Ціна занадто довга — обмеження 30 символів");
                    return;
                }

                if (!decimal.TryParse(priceText, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal price))
                {
                    MarkTextBoxInvalid(priceTextBox);
                    MessageBox.Show("Неможливо розпізнати число. Можливо, воно занадто велике.");
                    return;
                }

                if (price <= 0)
                {
                    MarkTextBoxInvalid(priceTextBox);
                    MessageBox.Show("Ціна має бути більше нуля");
                    return;
                }


                if (items.Count >= 5)
                {
                    MessageBox.Show("Максимум 5 позицій)");
                    return;
                }

                items.Add(new MenuItem
                {
                    Description = description,
                    Price = price
                });
                FlashLastRow();

                descriptionTextBox.Clear();
                priceTextBox.Clear();
                UpdateTotals();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при додаванні позиції: {ex.Message}", "Помилка");
            }

 
        }
        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            if (itemsDataGrid.SelectedItem is MenuItem selectedItem)
            {
                items.Remove(selectedItem);
                UpdateTotals();
                MessageBox.Show("Позиція успішно видалена");
            }
            else
            {
                MessageBox.Show("Оберіть позицію для видалення");
            }
        }


        private void Window_Closing(object? sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBox.Show("Дякую, що користувалися додатком 🍰\nГарного дня!", "Допобачення",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void ExitApp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Помилка при закритті: {ex.Message}");
            }
        }
        private void ShowNotification(string message)
        {
            notificationText.Text = message;
            notificationText.Visibility = Visibility.Visible;
            notificationText.Opacity = 1;

            var fadeOut = new System.Windows.Media.Animation.DoubleAnimation(1, 0, TimeSpan.FromSeconds(2));
            fadeOut.BeginTime = TimeSpan.FromSeconds(1); // Подожди 1 секунду перед затуханием

            fadeOut.Completed += (s, e) =>
            {
                notificationText.Visibility = Visibility.Collapsed;
            };

            notificationText.BeginAnimation(OpacityProperty, fadeOut);
        }
        private void UpdateTotals()
        {
            decimal net = 0;
            try
            {
                net = BillCalculator.CalculateNetTotal(items);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Помилка при розрахунку суми: " + ex.Message);
                return;
            }

            decimal gst = BillCalculator.CalculateGst(net);
            decimal total = BillCalculator.CalculateTotal(net, tipAmount, gst);

            AnimateTextChange(netTotalText, $"Net Total: {net:C}");
            AnimateTextChange(tipAmountText, $"Tip: {tipAmount:C}");
            AnimateTextChange(gstAmountText, $"GST: {gst:C}");
            AnimateTextChange(totalAmountText, $"Total: {total:C}");
        }
        private void AnimateTextChange(System.Windows.Controls.TextBlock target, string newText)
        {
            var fadeOut = new DoubleAnimation(1, 0, TimeSpan.FromMilliseconds(200));
            fadeOut.Completed += (s, e) =>
            {
                target.Text = newText;

                var fadeIn = new DoubleAnimation(0, 1, TimeSpan.FromMilliseconds(200));
                target.BeginAnimation(OpacityProperty, fadeIn);
            };

            target.BeginAnimation(OpacityProperty, fadeOut);
        }
        private async void FlashLastRow()
        {
            await Dispatcher.InvokeAsync(() =>
            {
                itemsDataGrid.UpdateLayout();
                itemsDataGrid.ScrollIntoView(itemsDataGrid.Items[items.Count - 1]);
            });

            var row = (System.Windows.Controls.DataGridRow)itemsDataGrid.ItemContainerGenerator.ContainerFromIndex(items.Count - 1);
            if (row != null)
            {
                var originalBrush = row.Background;
                row.Background = System.Windows.Media.Brushes.LightGreen;

                await Task.Delay(400); // Подсветка на 400 мс

                row.Background = originalBrush;
            }
        }
        private void MarkTextBoxInvalid(System.Windows.Controls.TextBox box)
        {
            box.BorderBrush = System.Windows.Media.Brushes.Red;
            box.BorderThickness = new Thickness(2);
        }
        private void ResetTextBoxStyle(System.Windows.Controls.TextBox box)
        {
            box.ClearValue(System.Windows.Controls.Border.BorderBrushProperty);
            box.ClearValue(System.Windows.Controls.Border.BorderThicknessProperty);
        }

    }
}
