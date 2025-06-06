﻿using System.ComponentModel.DataAnnotations;

namespace BlazorApp.Model;

public class Person
{
    public int Id { get; set; }

    [MaxLength(15)]
    public string FirstName { get; set; }
    public string LastName { get; set; }

    [EmailAddress]
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Address Address { get; set; }
    public Contract[] Contracts { get; set; }
}

public class Address
{
    public int Id { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public string ZipCode { get; set; }
}

public class Contract
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string PlateNumber { get; set; }
    public DateTime Signed { get; set; }
    public int CarBrand { get; set; }
    public string HexColor { get; set; }
}
