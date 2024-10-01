using FluentValidation;
using HopSkills.BackOffice.Client.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.RegularExpressions;
using HopSkills.BackOffice.Client.Controls;

namespace HopSkills.BackOffice.Client.Pages.Contents
{
    public partial class CreateGame
    {
        private string? serviceEndpoint;
        private string? backendUrl;
        private bool success;
        private bool disabledNext1 { get; set; }
        private bool disabledNext2 { get; set; }
        private bool form1Validate { get; set; }
        private bool form2Validate { get; set; }
        private bool form3Validate { get; set; }
        private bool formQuestions { get; set; }
        private bool form1IsValid { get; set; }
        private bool fileNameWrong { get; set; }
        private MudDropContainer<CreateMultipleQuestionsViewModel> _container;
        private string getPriorGamesEndpoint { get; set; }
        private CreateMultipleQuestionsViewModel _questionFormInput { get; set; }
        EditContext InputGameContext1;
        EditContext InputGameContext2;
        EditContext InputGameContext3;
        private List<AddQuestionFormComponent> addQuestionForms { get; set; }
        private CreateGameViewModel InputGame = new();
        string[] errors = { };
        MudForm form1 = new MudForm() { IsValid = false };
        MudButton next1 = new MudButton() { Disabled = true };
        MudButton next2 = new MudButton() { Disabled = true };
        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }
        private AuthenticationState authenticationState { get; set; }
        private ClaimsPrincipal authenticatedUser { get; set; }
        private List<GameViewModel>? Games;
        private string base64data = "";
        protected override async Task OnInitializedAsync()
        {
            addQuestionForms =
            [
                new() {
                    Id = 0,
                    questionFormInput = new CreateMultipleQuestionsViewModel
                {
                    PossibleAnswers =
                [
                    new CreateAnswerViewModel { Id = 1,
                        Label = "Answer 1",
                        Answer = string.Empty,
                    IsCorrect = false, Order = 0}
                ],
                ImageFiles = new List<string>(),
                AudioFiles = new List<string>()
                }
                }
            ];
            serviceEndpoint = $"{backendUrl}/api/Game/AddGame";
            InputGame.Image = new CreateGameImageViewModel();
            InputGameContext1 = new(InputGame);
            InputGameContext2 = new(InputGame);
            InputGameContext3 = new(InputGame);
            InputGameContext1.OnFieldChanged += InputGameContext1_OnFieldChanged;
            InputGame.multipleQuestions = [];
            form1Validate = true;
            disabledNext1 = true;
            disabledNext2 = true;
            fileNameWrong = false;
            if (authenticationStateTask is not null)
            {
                authenticationState = await authenticationStateTask;
                if (authenticatedUser is null)
                    authenticatedUser = authenticationState?.User;
            }
            Games = new List<GameViewModel>();
            if (authenticatedUser.IsInRole("Admin"))
                getPriorGamesEndpoint = $"{backendUrl}/api/Game/GetAll";
            else
            {
                var user = await Http.GetFromJsonAsync<UserViewModel>($"{backendUrl}/api/User/userdetails/{authenticatedUser.Identity.Name}");
                getPriorGamesEndpoint = $"{backendUrl}/api/Game/getgamesbycustomer/{user.Company}";
            }
            if (!string.IsNullOrEmpty(serviceEndpoint))
                Games = await Http.GetFromJsonAsync<List<GameViewModel>>(getPriorGamesEndpoint);
        }

        private void InputGameContext1_OnFieldChanged(object sender, FieldChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty(InputGame.Title)
            && !String.IsNullOrEmpty(InputGame.DifficultyLevel)
            && !String.IsNullOrEmpty(InputGame.Theme)
            && !String.IsNullOrEmpty(InputGame.ElligibleSub) && !fileNameWrong)
                disabledNext1 = false;
            if (!String.IsNullOrEmpty(InputGame.Description))
                disabledNext2 = false;
            StateHasChanged();
        }

        protected async Task CreateFormCanceled(int Id)
        {
            addQuestionForms.Remove(addQuestionForms[Id]);
            //formQuestions = !formQuestions;
            //form3Validate = !form3Validate;
            StateHasChanged();
        }

        private void RefreshContainer()
        {
            //update the binding to the container
            StateHasChanged();

            //the container refreshes the internal state
            _container.Refresh();
        }

        protected async void CreateFormValid(CreateMultipleQuestionsViewModel validQuestionFom)
        {
            addQuestionForms.LastOrDefault().questionFormInput = validQuestionFom;
            addQuestionForms.LastOrDefault().questionFormInput.Zone = _zones[0].Name;
            InputGame.multipleQuestions.Add(addQuestionForms.LastOrDefault().questionFormInput);
            InputGame.TotalXperience += addQuestionForms.LastOrDefault().questionFormInput.Xperience;
            InputGame.TotalDuration.Add(TimeSpan.Parse(addQuestionForms.LastOrDefault().questionFormInput.Duration));
            addQuestionForms.LastOrDefault().IsValid = true;
            Snackbar.Add("Question Form Has Been Added Successfully", MudBlazor.Severity.Success);
            formQuestions = !formQuestions;
            form3Validate = !form3Validate;
        }

        private void AddQuizz()
        {
            if(addQuestionForms is not null && addQuestionForms.Count != 0)
            {
                if(addQuestionForms.LastOrDefault().IsValid)
                {
                    addQuestionForms.LastOrDefault().Expanded = false;
                    addQuestionForms.Add(new AddQuestionFormComponent()
                    { Id = addQuestionForms.Count+1,
                        questionFormInput = new CreateMultipleQuestionsViewModel
                        {
                            PossibleAnswers =
                [
                    new CreateAnswerViewModel { Id = 1,
                        Label = "Answer 1",
                        Answer = string.Empty,
                    IsCorrect = false, Order = 0}
                ],
                            ImageFiles = [],
                            AudioFiles = []
                        }
                    });
                }
                else
                    Snackbar.Add("Please fill or delete the last quiz first", MudBlazor.Severity.Error);
            }
        }

        private void OnValidSubmit(EditContext context)
        {
            if (form1Validate)
            {
                form1Validate = false;
                form2Validate = true;
            }
            else if (form2Validate)
            {
                form2Validate = false;
                form3Validate = true;
            }
            StateHasChanged();
        }

        private void DisplayFormQuestions()
        {
            form2Validate = false;
            formQuestions = !formQuestions;
        }

        private void NavigateToPreviousGameForm()
        {
            if (form2Validate)
            {
                form1Validate = true;
                form2Validate = false;
            }
            else if (form3Validate)
            {
                form2Validate = true;
                form3Validate = false;
            }
        }

        private async void SaveGame(int Id)
        {
            InputGame.Status = Id == 0 ? "Draft" : "Published";
            InputGame.Creator = authenticatedUser.Identity.Name; ;
            var result = await Http.PostAsJsonAsync(serviceEndpoint, InputGame);
            Navigation.NavigateTo("/games");
        }

#nullable enable
        public class Model
        {
            public IReadOnlyList<IBrowserFile>? Files { get; set; } = new List<IBrowserFile>();
        }

        private Model _model = new();
        private ModelFluentValidator _validationRules = new();
        private MudFileUpload<IReadOnlyList<IBrowserFile>>? _fileUpload;
        private bool _isValid;
        private bool _isTouched;
        private const string FileContent = "this is content";
        private const string DefaultDragClass = "relative rounded-lg border-2 border-dashed pa-4 mt-4 mud-width-full mud-height-full";
        private string _dragClass = DefaultDragClass;

        private async void LoadFiles(InputFileChangeEventArgs e)
        {
            var regexItem = new Regex("^[a-zA-Z0-9_.-]*$");
            InputGame.Image.Title = e.File.Name.ToString();
            if (regexItem.IsMatch(e.File.Name.ToString()))
            {
                fileNameWrong = false;
                var format = e.File.ContentType;
                var resizeImage = await e.File.RequestImageFileAsync(format, 300, int.MaxValue);
                var buffer = new byte[resizeImage.Size];
                await resizeImage.OpenReadStream().ReadAsync(buffer);
                InputGame.Image.Content = Convert.ToBase64String(buffer);
                base64data = "data:image/png;base64," + InputGame.Image.Content;
                StateHasChanged();
            }
            else
            {
                fileNameWrong = true;
                disabledNext1 = true;
            }
        }

        private void Upload()
        {
            // Upload the files here
            Snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
            Snackbar.Add("TODO: Upload your files!");
        }

        private void SetDragClass()
            => _dragClass = $"{DefaultDragClass} mud-border-primary";

        private void ClearDragClass()
            => _dragClass = DefaultDragClass;

        private Task OpenFilePickerAsync()
            => _fileUpload?.OpenFilePickerAsync() ?? Task.CompletedTask;

        private Task ClearAsync()
        {
            InputGame.Image = new CreateGameImageViewModel();
            fileNameWrong = false;
            _fileUpload?.ClearAsync();
            StateHasChanged();
            return Task.CompletedTask;
        }

        public class ModelFluentValidator : AbstractValidator<Model>
        {
            public ModelFluentValidator()
            {
                RuleFor(x => x.Files)
                    .NotEmpty()
                    .WithMessage("There must be at least 1 file.");
            }

            public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
            {
                var result = await ValidateAsync(ValidationContext<Model>.CreateWithOptions((Model)model, x => x.IncludeProperties(propertyName)));
                return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
            };
        }

        RegisterAccountForm model = new RegisterAccountForm();

        public class RegisterAccountForm
        {
            [Required]
            [StringLength(8, ErrorMessage = "Name length can't be more than 8.")]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [StringLength(30, ErrorMessage = "Password must be at least 8 characters long.", MinimumLength = 8)]
            public string Password { get; set; }

            [Required]
            [Compare(nameof(Password))]
            public string Password2 { get; set; }

        }

        private void ItemUpdated(MudItemDropInfo<DropZoneItem> dropItem) => dropItem.Item.Zone = dropItem.DropzoneIdentifier;

        private List<DropZone> _zones = new()
    {
        new() { Name = "Drop Zone 1" }
    };

        private List<DropZoneItem> _items = new()
    {
        new() { Zone = "Drop Zone 1", Name = "Item 1" },
        new() { Zone = "Drop Zone 1", Name = "Item 2" },
        new() { Zone = "Drop Zone 2", Name = "Item 3" },
    };

        private class DropZone
        {
            public string Name { get; init; }
        }

        private class DropZoneItem
        {
            public string Zone { get; set; }
            public string Name { get; init; }
        }
    }
}
