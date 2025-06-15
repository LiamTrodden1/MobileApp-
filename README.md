# 6002CEM_LiamTrodden-NEW-

## Title: AlbumApp
### Justification
Spotify is a popular music streaming application that is used by many people. One well known feature of Spotify is Spotify wrapped. Spotify wrapped is a statistical summary of your listening habits throughout a year. With the recent resurgence of music media formats like vinyl, I would like to combine the statistical fact gathering of Spotify’s Spotify wrapped with physical media in mind, to produce a mobile application that can track statistics of physical media formats efficiently. Due to this project focusing on physical media, the mobile application will focus primarily on albums not songs, as physical media is most common in an album format. Obviously, these statistics will not be as truthful as Spotify, because the tracked statistics rely on the users honestly with the application to not lie about what they have listened to, and the app can’t tell if the user actually listened to the album.
### App Navigation
The main application uses both a flyout menu and a tab bar to navigate between grouped pages.
### Account Features
In terms of account-based features, this application has dedicated pages for creating accounts, logging in and updating account credentials in the settings. The account features use Google Firebase, which is an API and cloud storage platform to store user account credentials and authenticate users. The login page needs an email and password input and a submit details button. The create account page is the same, except it requires more user validation by inputting the password twice to ensure the credentials they input are correct. The password change also has 2 password input fields to verify the password they want to change to is correct, but it also requires the original password to be input and correct to validate that the owner of the account is making the password change request. 
### Dashboard
Upon a successful login, the user is presented with the dashboard, which provides a carousel view of the 3 most recently added albums, and some summary statistics like most listened to album and total albums listened.
The user can also logout from the dashboard using a toolbar item. 
### Album Pages
The album page is where users can make requests to the Spotify API to acquire albums and related statistics about the album. Once the user is happy with the selected album, they can submit the album to be saved to the database. The user is supposed to save an album once they have listened to the album in real life for the app to track the album statistics. Duplicate saves of the same album increase the count of the amount of times the album was listened to.
The album management page is a stack view of the database that presents all listened to albums in a visually pleasing way. In this page, the user can remove the count of an album using a swipe to delete button, in case they added the album too many times or want to remove it from their listened to list. If the count reaches 0, the album is removed from the database and user interface.
The app preferences page allows the user to select their favourite album by name from their listened to albums stored in the database. The favourite album is then saved locally and displayed in preferences.


