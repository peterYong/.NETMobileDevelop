using Notes.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteEntryPageFromSQLite : ContentPage
    {
        public NoteEntryPageFromSQLite()
        {
            InitializeComponent();
        }
        

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var note = (NoteData)BindingContext;
            note.Date = DateTime.UtcNow;
            await App.Database.UpdateNoteAsync(note);
            await Navigation.PopAsync();
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var note = (NoteData)BindingContext;
            await App.Database.DeleteNoteAsync(note);
            await Navigation.PopAsync();
        }

        
    }
}