Help Guide:
1. How to get your Yahoo Finance API Key. (https://www.educative.io/answers/how-to-get-the-api-key-for-yh-finance-api)
2. How to set up your sender email and get application password. (https://support.google.com/mail/answer/185833?hl=en)
3. In case of SMTP Server problems, enable Telnet Client to test the connection. (https://www.quora.com/How-do-I-enable-or-install-Telnet-on-Windows-10-or-11)
4. Access config.json file and insert credentials.
5. Build the project.
6. Open terminal on project root and run:                 

       ./StockPriceNotificator.exe {AssetAbbreviation[string]} {SellPrice[decimal]} {BuyPrice[decimal]}

Inputs:
- Abbreviation: New York Stock Exchange (NYSE)
- Currency: USD 


Possible new features:
1. Allow list of emails to receive the alert email.
2. Configure a Kafka Producer to be able to consume the topic.
3. Interface + Web API with topic Consumer.
