using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using PizzaDelivery.Data;
using PizzaDeliveryDashboard.Services;

namespace PizzaDeliveryDashboard.Customers
{
    public partial class CustomerEditView : UserControl
    {
        private ICustomersRepository _repository = new CustomersRepository();
        private Customer _customer = null;

        public CustomerEditView()
        {
            InitializeComponent();
        }

        public Guid CustomerId
        {
            get { return (Guid)GetValue(CustomerIdProperty); }
            set { SetValue(CustomerIdProperty, value); }
        }

        public static readonly DependencyProperty CustomerIdProperty =
            DependencyProperty.Register("CustomerId", typeof(Guid), 
            typeof(CustomerEditView), new PropertyMetadata(Guid.Empty));

        private async void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;

            _customer = await _repository.GetCustomerAsync(CustomerId);
            if (_customer == null) return;
            firstNameTextBox.Text = _customer.FirstName;
            lastNameTextBox.Text = _customer.LastName;
            phoneTextBox.Text = _customer.Phone;
        }

        private async void OnSave(object sender, RoutedEventArgs e)
        {
            // TODO: Validate input... call business rules... etc...
            _customer.FirstName = firstNameTextBox.Text;
            _customer.LastName = lastNameTextBox.Text;
            _customer.Phone = phoneTextBox.Text;
            await _repository.UpdateCustomerAsync(_customer);
        }
    }
}
