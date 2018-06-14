using NUnit.Framework;

namespace NetTopologySuite.Tests.Various
{
    using GeoAPI.Geometries;
    using Geometries;
    using NetTopologySuite.IO;

    [TestFixture]
    public class Issue58Tests
    {
        [Test, Category("Issue58")]
        public void IsValidTest1()
        {
            Execute(GeometryFactory.Floating);
        }

        [Test, Category("Issue58")]
        public void IsValidTest2()
        {
            Execute(GeometryFactory.FloatingSingle);
        }

        [Test, Category("Issue58")]
        public void IsValidTest3()
        {
            Execute(GeometryFactory.Fixed);
        }

        private void Execute(IGeometryFactory factory)
        {
            const string wkt1 = @"POLYGON ((34.8998882099012 30.3837960942026, 34.9010566737651 30.3644568453302, 34.952062587058 30.3841960548712, 34.9237694926759 30.3379967024143, 34.8688043479468 30.2722872698552, 34.8675239767921 30.2729668362851, 34.8644624659558 30.2746560404891, 34.8614352803289 30.2763912924804, 34.8584433396252 30.2781720660657, 34.8554875530179 30.2799978212197, 34.8525688188649 30.2818680042466, 34.8496880244379 30.2837820479458, 34.8468460456542 30.2857393717817, 34.8440437468117 30.2877393820578, 34.8412819803279 30.2897814720941, 34.8385615864812 30.291865022409, 34.8358833931563 30.2939894009049, 34.8332482155932 30.2961539630577, 34.8306568561393 30.2983580521099, 34.8281101040059 30.3006009992674, 34.8256087350277 30.3028821239005, 34.8231535114263 30.3052007337477, 34.8207451815784 30.3075561251236, 34.8183844797864 30.3099475831299, 34.8160721260548 30.3123743818706, 34.813808825869 30.3148357846692, 34.8115952699799 30.3173310442906, 34.8094321341915 30.3198594031656, 34.8073200791532 30.3224200936183, 34.8052597501567 30.3250123380972, 34.8038593775008 30.3268416420348, 34.8413134524996 30.34133631112, 34.8380456129141 30.3420679436136, 34.8345469119455 30.3429079098264, 34.8310655827664 30.3438006160548, 34.827824169131 30.3446853380141, 34.827602680711 30.3447457917824, 34.8241592556176 30.3457431505854, 34.8207363515157 30.3467923902175, 34.8173350063135 30.3478931927008, 34.8139562514873 30.34904522442, 34.8106011117732 30.3502481362226, 34.80727060486 30.3515015635225, 34.8039657410851 30.3528051264095, 34.8006875231308 30.3541584297621, 34.7974369457248 30.355561063366, 34.8114061852091 30.3793421607384, 34.8098261518056 30.3806388748041, 34.8072325551456 30.3828429255909, 34.8046836035397 30.3850858339783, 34.8021800734927 30.387366919357, 34.7997227278858 30.3896854894863, 34.797312315744 30.3920408407015, 34.7949495720071 30.3944322581256, 34.7926352173053 30.3968590158832, 34.7903699577383 30.3993203773193, 34.7881544846595 30.4018155952199, 34.8175519633847 30.421036532238, 34.8473374156124 30.4405111377535, 34.8473374156124 30.4405111377536, 34.8945946797018 30.4714093282142, 34.89504284421 30.4639917561117, 34.9249010902965 30.4502666241023, 34.9120339400511 30.4239712613602, 34.8990624811609 30.3974627330299, 34.8990624811609 30.3974627330298, 34.8998882099012 30.3837960942026), (34.8998882099012 30.3837960942026, 34.8896581575596 30.3782440174851, 34.8896581575596 30.3782440174851, 34.8998882099012 30.3837960942026))";
            const string wkt2 = @"POLYGON ((34.89504284421 30.4639917561117, 34.8974881525283 30.4235194508906, 34.8362388038152 30.4216169690234, 34.8683403196915 30.4762662772922, 34.89504284421 30.4639917561117))";

            var reader = new WKTReader(factory);

            var g1 = reader.Read(wkt1);
            Assert.IsFalse(g1.IsValid);
            var v1 = g1.Buffer(0);
            Assert.IsTrue(v1.IsValid);

            var g2 = reader.Read(wkt2);
            //Assert.IsFalse(g2.IsValid);
            var v2 = g2.Buffer(0);
            Assert.IsTrue(v2.IsValid);

            var union1 = g1.Union(g2);
            Assert.IsNotNull(union1);
            Assert.IsTrue(union1.IsValid);

            var union2 = v1.Union(v2);
            Assert.IsNotNull(union2);
            Assert.IsTrue(union2.IsValid);
        }
    }
}