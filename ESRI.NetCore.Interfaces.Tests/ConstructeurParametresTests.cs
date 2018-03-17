using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ESRI.NetCore.Interfaces.Tests
{
    [TestClass]
    public class ConstructeurParametresTests
    {
        /// <summary>
        /// Injection de dépendances.
        /// </summary>
        private IServiceProvider _serviceCollection = new ServiceCollection()
            .AddScoped<IConstructeurParametres, ConstructeurParametres>()
            .BuildServiceProvider();

        /// <summary>
        /// Ajout d'une condition where.
        /// </summary>
        [TestMethod]
        [TestCategory(@"ConstructeurParametres")]
        public void ConstruireAvecUneConditionWhere_Succes()
        {
            // Variables de travail.
            var constructeur = _serviceCollection.GetService<IConstructeurParametres>();

            // Attendu.
            var attendu = "ID = 1";

            // Actuel.
            var actuel = constructeur
                .AjouterConditionsWhere(attendu)
                .ConstruireParametresRequete()
                .Where;

            // Assert.
            Assert.AreEqual(attendu, actuel);
        }

        /// <summary>
        /// Ajout d'une condition where avec plusieurs conditions.
        /// </summary>
        [TestMethod]
        [TestCategory(@"ConstructeurParametres")]
        public void ConstruireAvecPlusieursConditionWhere_Succes()
        {
            // Variables de travail.
            var constructeur = _serviceCollection.GetService<IConstructeurParametres>();

            // Attendu.
            var c1 = "ID = 1";
            var c2 = "DESC = 'tata'";
            var attendu = $"{c1} AND {c2}";

            // Actuel.
            var actuel = constructeur
                .AjouterConditionsWhere(c1, c2)
                .ConstruireParametresRequete()
                .Where;

            // Assert.
            Assert.AreEqual(attendu, actuel);
        }

        /// <summary>
        /// Ajout d'une condition pour afficher tous les champs.
        /// </summary>
        [TestMethod]
        [TestCategory(@"ConstructeurParametres")]
        public void ConstruireAvecTousLesChampsSortie_Succes()
        {
            // Variables de travail.
            var constructeur = _serviceCollection.GetService<IConstructeurParametres>();

            // Attendu.
            var attendu = true;

            // Actuel.
            var actuel = constructeur
                .AfficherTousLesChamps()
                .ConstruireParametresRequete()
                .AfficherTousLesChamps;

            // Assert.
            Assert.AreEqual(attendu, actuel);
        }
    }
}