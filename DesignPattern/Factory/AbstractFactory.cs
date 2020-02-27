using System;

namespace CsharpBasic.DesignPattern.Factory {
    public interface IAbstractFactory1 {
        ICPUApi CreateCPUApi ();
        IMainboardApi CreateMainboardApi ();
    }

    public class Schema1 : IAbstractFactory1 {
        public ICPUApi CreateCPUApi () {
            return new IntelCPUApi (1561);
        }
        public IMainboardApi CreateMainboardApi () {
            return new GAMainboardApi (1561);
        }
    }

    public class Schema2 : IAbstractFactory1 {
        public ICPUApi CreateCPUApi () {
            return new AMDCPUApi (1382);
        }
        public IMainboardApi CreateMainboardApi () {
            return new MSIMainboardApi (1382);
        }
    }
    /* #endregion */

    /* #region  product interface and impletments */
    public interface ICPUApi {
        void Calculate ();
    }

    public interface IMainboardApi {
        void InstallCPU ();
    }

    public class IntelCPUApi : ICPUApi {
        private int pins;

        public IntelCPUApi (int pins) {
            this.pins = pins;
        }

        public void Calculate () {
            Console.WriteLine ($"Intel CPU has {pins} pins");
        }
    }

    public class AMDCPUApi : ICPUApi {
        private int pins;

        public AMDCPUApi (int pins) {
            this.pins = pins;
        }

        public void Calculate () {
            Console.WriteLine ($"AMD CPU has {pins} pins");
        }
    }

    public class GAMainboardApi : IMainboardApi {
        private int slots;

        public GAMainboardApi (int slots) {
            this.slots = slots;
        }

        public void InstallCPU () {
            Console.WriteLine ($"GA Mainboard has {slots} slots");
        }
    }

    public class MSIMainboardApi : IMainboardApi {

        private int slots;

        public MSIMainboardApi (int slots) {
            this.slots = slots;
        }

        public void InstallCPU () {
            Console.WriteLine ($"MSI Mainboard has {slots} slots");
        }
    }
    /* #endregion */

    /* #region computer engineer(executor) */
    public class ComputerEngineer {
        private ICPUApi cpuApi = null;
        private IMainboardApi mainboardApi = null;

        private void PrepareHardware (IAbstractFactory1 schema) {
            cpuApi = schema.CreateCPUApi ();
            mainboardApi = schema.CreateMainboardApi ();

            cpuApi.Calculate ();
            mainboardApi.InstallCPU ();
        }


        public void MakeComputer (IAbstractFactory1 schema) {

            PrepareHardware (schema);
        }
    }
    /* #endregion */

    /* #region  Test */
     public partial class TestFactory {

        public void TestAbstractFactory () {

            IAbstractFactory1 schema1 = new Schema1 ();
            ComputerEngineer engineer = new ComputerEngineer ();
            engineer.MakeComputer (schema1);

            IAbstractFactory1 schema2 = new Schema2 ();
            engineer.MakeComputer (schema2);

            IAbstractFactory2 schema3 = new Schema3 ();
            var engieer1 = new ComputerEngineer1 ();
            engieer1.MakeComputer (schema3);

            IAbstractFactory2 schema4 = new Schema4 ();
            engieer1.MakeComputer (schema4);

            IAbstractFactory2 schema5 = new Schema5 ();
            engieer1.MakeComputer (schema5);
        }
    }
    /* #endregion */

    ///--------------------------------------------------------------------------------------------------------------------------------
    // Extended Abstract Factory which is more flexible
    //---------------------------------------------------------------------------------------------------------------------------------

    public interface IAbstractFactory2 {
        Object CreateApi (int type);
    }

    public class Schema3 : IAbstractFactory2 {

        public Object CreateApi (int type) {
            Object obj = null;

            switch (type) {
                case 1:
                    return new AMDCPUApi (1234);
                case 2:
                    return new GAMainboardApi (1234);
            }

            return obj;
        }
    }

    public class Schema4 : IAbstractFactory2 {

        public Object CreateApi (int type) {
            Object obj = null;

            switch (type) {
                case 1:
                    return new IntelCPUApi (4567);
                case 2:
                    return new MSIMainboardApi (4567);
            }

            return obj;
        }
    }

    public class ComputerEngineer1 {
        private ICPUApi cpuApi = null;
        private IMainboardApi mainboardApi = null;

        private IMemoryApi memoryApi = null; //add for the addition of interface of IMemoryApi

        void PrepareHardware (IAbstractFactory2 schema) {
            cpuApi = (ICPUApi) schema.CreateApi (1);
            mainboardApi = (IMainboardApi) schema.CreateApi (2);
            memoryApi = (IMemoryApi)schema.CreateApi(3);

            cpuApi.Calculate ();
            mainboardApi.InstallCPU ();

            if (memoryApi != null) // Add For addtional interface of IMemoryApi
                memoryApi.RunMem();
        }

        public void MakeComputer (IAbstractFactory2 schema) {
            PrepareHardware (schema);
        }
    }

    /* #region  Add a memory interface */
    public interface IMemoryApi {
        void RunMem ();
    }

    public class KingstonMemoryApi : IMemoryApi {
        private int mainFrequency;

        public KingstonMemoryApi (int mf) {
            this.mainFrequency = mf;
        }

        public void RunMem () {
            Console.WriteLine ($"Memory runs at {mainFrequency}");
        }
    }

    public class Schema5 : IAbstractFactory2 {

        public Object CreateApi (int type) {
            Object obj = null;

            switch (type) {
                case 1:
                    return new IntelCPUApi (6789);
                case 2:
                    return new MSIMainboardApi (6789);
                case 3:
                    return new KingstonMemoryApi (6789);
            }

            return obj;
        }
    }
    /* #endregion */

}