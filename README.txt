
RUN Database.sql to create RapidPay database.

Modify RapidPayEntities connection string in Web.config file with the data source and proper credentials.

Update "interval" in PaymentFee.cs in Services folder to adjust time (in seconds) to calculate fee.

RapdiPayAPI

token: used to request access authorization. The "access_token" should be used in the header of each method. The username and password must exist in RPUsers table

/token

grant_type:password
username:{string}
password:{string}

Result:

{
    "access_token": "O44aW7agzItwaYtV0L9keH11XpPf-PSEHd1r3VQIz_FyT-7D_9H1tQwPO8kJ1_DKU0WY7PIl5tizPU1EpliB0REqNHvcLUpOo0sTcQT4P94WYu1sLnOMpnhIoZ2EWm2vEs9OTJOfYq_QNtNl__t3m2QvC6RzkErKAW3lf7C34Gxo5AFPYI5LQnVA20bUx14haUwE7UTgtNgLgXpnMOGdxt48w6zMYtYhKfjK-mTu4wMzS5SSW3gv7hKEn6CojbrvR_CAaUOiKq19mbeaOdeZYB1SNYzsm25qZrR56GmzKuH4eNxiHoNe54Bl2xSHge3oeRKTCRivd8-ReOb2zOyMLw",
    "token_type": "bearer",
    "expires_in": 3599,
    "userName": "06a5b881378c4f8beeab11038890d44d84a98b1640dc2562c77566bae13d3b1eb1c7f7c3ed7bf15a764dc8babde0d5685e0b6b6d368fc58a20262b0687a5dd46",
    ".issued": "Fri, 11 Feb 2022 22:27:11 GMT",
    ".expires": "Fri, 11 Feb 2022 23:27:11 GMT"
}

CreateCard: used to create a new card. The card format must have 15 digits. When the method is used, it's validated that the card number has not been used previously.

POST api/CardManagement/CreateCard?CardNumber={CardNumber}&CreditLimit={CreditLimit}

Result:

{
    "Result": "Card added sucessfully."
}

GetFee: used to query the current fee amount.

GET api/CardManagement/GetFee

Result:

{
    "Result": "payment fee 0.185572795754706 at 2/11/2022 4:34:13 PM. Last update 2/11/2022 4:33:45 PM."
}

Pay: used to create a new payment. When the method is used, it's validated that the card number exists and the balance must be greater than the amount + the fee.

POST api/CardManagement/Pay?CardNumber={CardNumber}&Amount={Amount}

Result:

{
    "Result": "Payment created sucessfully."
}

GetBalance: used to retrieve current balance of a card number.

GET api/CardManagement/GetBalance?CardNumber={CardNumber}

Result:

{
    "CardNumber": "123456789000007",
    "Balance": 0.00,
    "CreditLimit": 100.00,    
}