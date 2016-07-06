using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.ApplicationModel.Email;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EmailandSms
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            //email
            StorageFolder picturesFolder = KnownFolders.PicturesLibrary;
            StorageFile attachmentFile= await picturesFolder.GetFileAsync("dog.jpg");
             ComposeEmail("645648042@qq.com", "Hola,Te quiero", attachmentFile);
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Sms
            StorageFolder picturesFolder = KnownFolders.PicturesLibrary;
            StorageFile attachmentFile = await picturesFolder.GetFileAsync("dog.jpg");
            ComposeSms("051085608775", "Hola Te quiero", attachmentFile, ".jpg");
        }
        private async void ComposeEmail(String address,
         string messageBody,
         StorageFile attachmentFile)
        {
            var emailMessage = new Windows.ApplicationModel.Email.EmailMessage();
            emailMessage.Body = messageBody;

            if (attachmentFile != null)
            {
                var stream = Windows.Storage.Streams.RandomAccessStreamReference.CreateFromFile(attachmentFile);

                var attachment = new Windows.ApplicationModel.Email.EmailAttachment(
                    attachmentFile.Name,
                    stream);

                emailMessage.Attachments.Add(attachment);
            }
            emailMessage.To.Add(new EmailRecipient(address));
            //var email = recipient.Emails.FirstOrDefault<Windows.ApplicationModel.Contacts.ContactEmail>();
            //if (email != null)
            //{
            //    var emailRecipient = new Windows.ApplicationModel.Email.EmailRecipient(address);
            //    emailMessage.To.Add(emailRecipient);
            //}

            await Windows.ApplicationModel.Email.EmailManager.ShowComposeNewEmailAsync(emailMessage);

        }

        private async void ComposeSms(string num,
         string messageBody,
         StorageFile attachmentFile,
         string mimeType)
        {
            var chatMessage = new Windows.ApplicationModel.Chat.ChatMessage();
            chatMessage.Body = messageBody;

            if (attachmentFile != null)
            {
                var stream = Windows.Storage.Streams.RandomAccessStreamReference.CreateFromFile(attachmentFile);

                var attachment = new Windows.ApplicationModel.Chat.ChatMessageAttachment(
                    mimeType,
                    stream);

                chatMessage.Attachments.Add(attachment);
            }

            //var phone = recipient.Phones.FirstOrDefault<Windows.ApplicationModel.Contacts.ContactPhone>();
            //if (phone != null)
            //{
            //    chatMessage.Recipients.Add(phone.Number);
            //}
            chatMessage.Recipients.Add(num);
            await Windows.ApplicationModel.Chat.ChatMessageManager.ShowComposeSmsMessageAsync(chatMessage);
        }


    }
}
