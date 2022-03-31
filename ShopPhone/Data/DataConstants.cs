namespace ShopPhone.Data
{
    public class DataConstants
    {

        public class Phone
        {
            public const int PhoneBrandMaxLength = 30;
            public const int PhoneBrandMinLength = 2;

            public const int PhoneModelMaxLength = 50;
            public const int PhoneModelMinLength = 2;

            public const int PhoneYearMinValue = 2000;
            public const int PhoneYearMaxValue = 2050;

            public const int PhoneDescriptionMinLength = 10;
            public const int PhoneDescriptionMaxLength = 10000;
        }

        public class Owner
        {
            public const int OwnerNameMaxLength = 30;
            public const int OwnerNameMinLength = 2;
                            
            public const int OwnerPhoneMaxValue = 20;
            public const int OwnerPhoneMinValue = 10;
                            
            public const int OwnerEmailMaxLength = 50;
            public const int OwnerEmailMinLength = 8;
        }

        public class Category
        {
            public const int CategoryNameMaxLength = 20;
            public const int CategoryNameMinLength = 2;
        }

        public class User
        {
            public const int UserNameMaxLength = 30;
            public const int UserNameMinLength = 2;

            public const int UserLastNameMaxLength = 30;
            public const int UserLastNameMinLength = 2;

            public const int UserEmailMaxLength = 50;
            public const int UserEmailMinLength = 2;

            public const int UserPhoneMaxValue = 20;
            public const int UserPhoneMinValue = 10;
        }

    }
}


