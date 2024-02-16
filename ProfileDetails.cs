public static class ProfileDetails
{
    private static List<ProfileDemo> _lstProfile=null;
    public static IEnumerable<ProfileDemo> getProfileData()
    {
        if(_lstProfile==null)
        {
            _lstProfile=new List<ProfileDemo>();
            _lstProfile.Add(new ProfileDemo(){ Id=1,FirstName="Jaydeep",LastName="Gurav",Email="jaydeep@gmail.com",PhoneNumber="9876543212",DOB=null,BiologicalInformation="",addr=new Address(){Street_Address="LBH Road",City="Mumbai",State_Province="Maharashtra",ZIP_PostalCode="400 051",Country="India"},AddInfo = new List<AdditionalInformation>(){new AdditionalInformation(){Id=1,Info="Default Info"}}});
        }
        return _lstProfile;
    }
}
