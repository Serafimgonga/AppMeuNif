using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Input;
using AppConsultaNif.Model;
using Microsoft.Maui.Controls;

namespace AppConsultaNif.ModelView
{
    public class ConsultaPageViewModel : INotifyPropertyChanged
    {
        private string? _numero;
        private Entity? _result;
        private bool _isBusy;
        private string? _errorMessage; // Add a property for error messages

        public string? Numero
        {
            get => _numero;
            set
            {
                _numero = value;
                OnPropertyChanged();
            }
        }

        public Entity? Result
        {
            get => _result;
            set
            {
                _result = value;
                OnPropertyChanged();
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public string? ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public ICommand? SubmitCommand { get; }
        public ICommand? ShowDetailsCommand { get; }


        public event PropertyChangedEventHandler? PropertyChanged;

        public ConsultaPageViewModel()
        {
            SubmitCommand = new Command(async () => await Submit());
            ShowDetailsCommand = new Command(ShowDetails);

        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private async void ShowDetails()
        {
            if (Result != null)
            {
                var jsonOptions = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };

                var json = JsonSerializer.Serialize(new
                {
                    success = true,
                    message = "",
                    data = Result
                }, jsonOptions);

              await Application.Current.MainPage.DisplayAlert("Detalhes do Registro", json, "OK");
            }
        }

        private async Task Submit()
        {
            if (IsBusy || string.IsNullOrEmpty(Numero))
            {
                ErrorMessage = "Numero do documento inválido";
                Result = null; // Hide the result layout
                return;
            }

            try
            {
                IsBusy = true;
                ErrorMessage = null; // Clear any previous error message

                var dataService = new GetData();
                Result = await Task.Run(() => dataService.Get(Numero));
            }
            catch (Exception ex)
            {
                // Handle the exception and set the error message
                ErrorMessage = ex.Message;
                Result = null; // Hide the result layout
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}