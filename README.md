# NoteSync
Prototype app to sync Obsidian documents with Confluence

## Sample appsettings.json
```json
{
    "Jira": {
        "Secret": "JIRASECRET",
        "Email": "email@email.com",
        "BaseURL": "https://.atlassian.net",
        "SpaceID": "12345678",
        "RootPageID": "1234567890"
    },
    "NoteDirectory": "~/notes/",
    "NoteExtension": ".md",
    "Sftp": {
        "Hostname": "sftp_iporhost",
        "Username": "sftp_user",
        "Password": "sftp_pass"
    }
}

```