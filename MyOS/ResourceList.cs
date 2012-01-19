using System;
using System.Collections.Generic;
using System.Text;

namespace MyOS
{
    class ResourceList
    {
        private static LinkedList<Resource> resources = new LinkedList<Resource>();

        /** Enlists all the resources available for the PCB's to use 
         * */
        public static void Initialize()
        {
            Resource rsc = null;
            rsc = new Resource(resources.Count, "Lights");
            resources.AddLast(rsc);
            rsc = new Resource(resources.Count, "Sounds");
            resources.AddLast(rsc);
            rsc = new Resource(resources.Count, "Aircon");
            resources.AddLast(rsc);
            rsc = new Resource(resources.Count, "Oven");
            resources.AddLast(rsc);
            rsc = new Resource(resources.Count, "Blender");
            resources.AddLast(rsc);
            rsc = new Resource(resources.Count, "Television");
            resources.AddLast(rsc);
            rsc = new Resource(resources.Count, "Stereo");
            resources.AddLast(rsc);
            rsc = new Resource(resources.Count, "Fan");
            resources.AddLast(rsc);
            rsc = new Resource(resources.Count, "Refrigerator");
            resources.AddLast(rsc);
            rsc = new Resource(resources.Count, "Rice Cooker");
            resources.AddLast(rsc);
        }

        public static int GetRandomNumber()
        {
            Random random = new Random();
            return (int)(random.NextDouble() * resources.Count);
        }

        /**Retrieves a given resource based on the index supplied.
         * The resource maybe removed from the list if so desired;
         * */
        public static Resource GetResource(int index, bool isRemove)
        {
            Resource[] rsc = new Resource[resources.Count];
            resources.CopyTo(rsc, 0);
            Resource temp = rsc[index];
            if (isRemove)
            {
                resources.Remove(temp);
            }
            return temp;
        }
    }
}
