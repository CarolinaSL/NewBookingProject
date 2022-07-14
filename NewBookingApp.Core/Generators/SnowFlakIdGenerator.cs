﻿using IdGen;

namespace NewBookingApp.Core.Generators
{
    public static class SnowFlakIdGenerator
    {
        private static IdGenerator _generator;

        public static void Configure(int generatorId)
        {

            var epoch = new DateTime(2021, 1, 1, 0, 0, 0, DateTimeKind.Local);

            // Create an ID with 45 bits for timestamp, 2 for generator-id
            // and 16 for sequence
            var structure = new IdStructure(45, 2, 16);

            // Prepare options
            var options = new IdGeneratorOptions(structure, new DefaultTimeSource(epoch));

            // Create an IdGenerator with it's generator-id set to 0, our custom epoch
            // and id-structure
            _generator = new IdGenerator(generatorId, options);
        }

        public static long NewId() => _generator.CreateId();
    }
}
