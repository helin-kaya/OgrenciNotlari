using Microsoft.AspNetCore.Identity;
using OgrenciNotlariEL.IdentityModels;
using OgrenciNotlariUL;
using Serilog.Core;

namespace OgrenciNotlariPL.CreateDefaultData
{
    public class CreateData
    {
        private readonly Logger _logger;

        public CreateData(Logger logger)
        {
            _logger = logger;
        }

        public void CreateRoles(IServiceProvider serviceProvider)
        {

            var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();
            //var emailManager = serviceProvider.GetRequiredService<AddressBook_BL.EmailSenderProcess.IEmailManager>();

            //var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            //var context = serviceProvider.GetService<AddressbookContext>();
            var configuration = serviceProvider.GetService<IConfiguration>();

            //string[] emails = configuration["ManagerEmails"].Split(',');
            var allRoles = Enum.GetNames(typeof(AllRoles));

            foreach (string role in allRoles)
            {
                var result = roleManager.RoleExistsAsync(role).Result; //rolden var mı?
                if (!result) //rolden yok!
                {
                    AppRole r = new AppRole()
                    {
                        InsertedDate = DateTime.Now,
                        Name = role,
                        IsDeleted = false,
                        Description = $"Sistem tarafından oluşturuldu"
                    };
                    var roleResult = roleManager.CreateAsync(r).Result;

                    //roleresulta bakalım
                    if (roleResult.Succeeded)
                    {
                        //foreach (var item in emails)
                        //{
                        //    //emailManager.SendEmail(new AddressBook_BL.EmailSenderProcess.EmailMessageModel()
                        //    //{
                        //    //    Body = $"Az önce sistem tarafından {r} rolü oluşturuldu",
                        //    //    Subject = "AddressBook Role Oluşturuldu",
                        //    //    To = item
                        //    //});
                        //}


                    }
                    else
                    {
                        //log email
                    }
                }

            }



        }
    }
}
