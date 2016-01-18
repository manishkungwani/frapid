# Mandrill Email API

When you start sending (several) hundreds of emails every day, you will soon realize that SMTP is not that efficient :

* The email conversion rate is low and declining.
* Most of your clients say that they do not get your emails at all.
* A few clients report back to you that your emails were found inside the "Junk" or "Spam" folder.
* An email server is not particularly happy with your speed of email submissions.
* An email server flags you as spammer, identifies you, and actively refuses your legitimate emails.

You lose business and this is a pretty sad.

![Mandrill Logo](images/mandrill.png)

Mandrill is a reliable, scalable, and secure delivery API for transactional emails from websites and applications. It's ideal for sending data-driven transactional emails, including targeted e-commerce and personalized one-to-one messages.
For up to 25000 emails per month, Mandrill charges you $9.95[after 2000 free trial sends](https://mandrill.com/pricing).

Since frapid has built-in support for Mandrill API, you just need to edit the configuration file `Mandrill.json` and you're good to go. 
Preferably, you can configure this from the [Admin Area](#) as well.

**~/Catalogs/<domain>/Configs/SMTP/Mandrill.json**
```json
{
    "FromName" : "",
    "FromEmail" : "",
	"ApiKey": "Your Mandrill API Key",
	"Enabled": false
}
```

## Configuration Explained

| Key                           | Configuration|
|-------------------------------|---------------------------------------------------------|
| FromName                      | The name field for the `FromEmail` key. |
| FromEmail                     | The from email address to be displayed to the email recipients.|
| ApiKey                        | The Mandrill API key. |
| Enabled                       | Set this to true if you want to use Mandrill API to send emails. If multiple email providers are enabled, the first one will be used. |


### Related Contents

* [Installing Frapid](../installation/README.md)
* [Developer Documentation](../developer/README.md)
* [Documentation Home](../../README.md)
