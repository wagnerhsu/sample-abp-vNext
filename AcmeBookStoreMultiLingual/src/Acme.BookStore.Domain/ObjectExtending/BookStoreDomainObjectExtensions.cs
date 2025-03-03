﻿using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.Threading;

namespace Acme.BookStore.ObjectExtending
{
    public static class BookStoreDomainObjectExtensions
    {
        private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

        public static void Configure()
        {
            OneTimeRunner.Run(() =>
            {
                /* You can configure extension properties to entities or other object types
                 * defined in the depended modules.
                 * 
                 * If you are using EF Core and want to map the entity extension properties to new
                 * table fields in the database, then configure them in the BookStoreEfCoreEntityExtensionMappings
                 *
                 * Example:
                 *
                 * ObjectExtensionManager.Instance
                 *    .AddOrUpdateProperty<IdentityRole, string>("Title");
                 *
                 * See the documentation for more:
                 * https://abp.io/docs/latest/Object-Extensions
                 */
            });
        }
    }
}