﻿namespace WebApplication1.Entities;

public class Country
{
    public int IdCountry { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<CountryTrip> CountryTrips { get; set; } = new List<CountryTrip>();
}