# HouseHoldManager
Simple calculator to manage electricity bills on my own ('cause I do not acheive any electricity bill at all).
It's not finished yet but skeleton is done:
1. data model
2. data access (via EF and MS SQL)
3. 'business logic' - calculator with extrapolation and interpolation ('cause measurements could be non regular)
4. integration tests - to check whether anything is working as intended
5. console app, which doesn't do anythin right now

Future plans:
1. enhance domain objects model - some decoration maybe
2. enhance data access - make some data validation on insert/request
3. add WPF clinet app which allows to add measurements/tariffs and renders reports
4. add utin tests for calculations, data validation and report generation
5. add authorization and restrictions on data/functionality access
6. add web client (could be mvc + api)
7. add mobile client for android ('cause I have a device on android)
    (could be xamarin + consuming api from 6. or c++ + android studio + consuming api from 6.)
8. host somewhere in order to get access via browser/mobile client

Somewhere between these 8 items - extend model and logic in order to process not only electricity bills.
