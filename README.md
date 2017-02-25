# _Barks_
#### A Mobile Dog Groomer

## By Brandon Cooper

# Description
A mock site to schedule your pets grooming with your favorite groomer.  We come to you.

# Specs

Stylist Specs:

- User can add Stylist
- User can view all available Stylists
- User can view Stylist details, customer list
- User can edit a Stylist
- User can delete a Stylist

Client Specs:

- User can add Client to a Stylist
- User can view all Clients
- User can edit a Clients name
- User can delete a Client

Database Creation:

-In SQLCMD
- CREATE DATABASE hair_salon;
- GO
- USE hair_salon;
- GO
- CREATE TABLE stylists (id INT IDENTITY(1,1), name VARCHAR(255), client_id - INT);
- CREATE TABLE clients ( id INT IDENTITY(1,1), name VARCHAR(255));
- GO

# Known Bugs
No known bugs.

## Legal
Licensed under the MIT License.

<a href="https://github.com/bcooper085/Hair_Salon_Cweek3">Github</a>
Copyright (c) 2017 Copyright Brandon Cooper, All Rights Reserved.
