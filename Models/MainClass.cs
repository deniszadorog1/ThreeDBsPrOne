using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using ThreeDbsPrOne.DBs;
using ThreeDbsPrOne.Models.Enums;

namespace ThreeDbsPrOne.Models
{
    public class MainClass
    {
        public List<User> Users { get; set; }
        public List<Resume> Resumes { get; set; }

        public MainClass()
        {
            Users = new List<User>();
            Resumes = new List<Resume>();
            FillParams();
        }

        public void FillParams()
        {
            Users = SsmsUsage.GetUsers();
            Resumes = SsmsUsage.GetResumes();
        }
        public City GetSityByString(string str)
        {
            for (int i = (int)City.Paris; i <= (int)City.Toronto; i++)
            {
                if (str.Equals(((City)i).ToString()))
                {
                    return (City)i;
                }
            }
            return City.Toronto;
        }

        public(List<int>, List<int>) GetUserAndResumeIdsToDelete()
        {
            //Get resumes with only one working place
            List<Resume> oneWorkResumes = GetResumesWithONEWorkPlace();

            List<int> resUserIds = GetUserIds(oneWorkResumes);
            List<int> resumesIds = GetResumesIdsWithSuchUserIds(resUserIds);

            return (resUserIds, resumesIds);
        }
        private List<int> GetResumesIdsWithSuchUserIds(List<int> userIds)
        {
            List<int> res = new List<int>();
            for(int i = 0; i < Resumes.Count; i++)
            {
                if (userIds.Contains(Resumes[i].UserId))
                {
                    res.Add(Resumes[i].Id);
                }
            }
            return res;
        }

        private List<int> GetUserIds(List<Resume> resumes)
        {
            List<int> res = new List<int>();

            for(int i = 0; i < resumes.Count; i++)
            {
                if (!res.Contains(resumes[i].UserId))
                {
                    res.Add(resumes[i].UserId);
                }
            }
            return res;
        }
        public List<Resume> GetResumesWithONEWorkPlace()
        {
            List<Resume> res = new List<Resume>();
            const int amountOfWorkPlaces = 1;
            for (int i = 0; i < Resumes.Count; i++)
            {
                if (Resumes[i].Institutions.Count == amountOfWorkPlaces)
                {
                    res.Add(Resumes[i]);
                }
            }
            return res;
        }
      


    }
}
