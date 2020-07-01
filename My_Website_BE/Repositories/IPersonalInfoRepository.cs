using My_Website_BE.Models;

namespace My_Website_BE.Repositories
{
    public interface IPersonalInfoRepository
    {
        PersonalInfo Create(PersonalInfo personalInfo);
        PersonalInfo Update(PersonalInfo personalInfoChanges);
        PersonalInfo GetPersonalInfo();

    }
}
