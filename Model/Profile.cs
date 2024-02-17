using System.ComponentModel.DataAnnotations;

public class ProfileDemo
{
    public int Id{get;set;}
    [Required]
    [MinLength(3)]
    [StringLength(50)]
    public string? FirstName{get;set;}
    [Required]
    [MinLength(3)]
    [StringLength(50)]
    public string? LastName{get;set;}
    [Required]
    [EmailAddress]
    public string? Email{get;set;}
    [Required]
    [Phone]
    [MinLength(10)]
    [MaxLength(10)]
    public string? PhoneNumber{get;set;}

    public Address addr{get;set;}=new();

    public DateTime? DOB{get;set;} 

    public string? BiologicalInformation{get;set;}

    public List<AdditionalInformation> AddInfo{get;set;}
}

public class Address{
    [Required]
    public string? Street_Address {get;set;}
    [Required]
    public string? City {get;set;}
    [Required]
    public string? State_Province {get;set;}
    [Required]
    public string? ZIP_PostalCode {get;set;}
    [Required]
    public string? Country {get;set;}
}

public class AdditionalInformation
{
    public int Id {get;set;}
    public string? Info {get;set;}
}