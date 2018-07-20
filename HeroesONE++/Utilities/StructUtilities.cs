/*
    [Reloaded] Mod Loader Launcher
    A universal, powerful multi-game, multi-process mod loader based on DLL Injection. 
    Copyright (C) 2018  Sewer. Sz (Sewer56)

    [Reloaded] is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    [Reloaded] is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <https://www.gnu.org/licenses/>
*/

/**
    Shamelessly nicked from my other project, except that this is a slice of the class
    used there.
*/

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace HeroesONE_R.Utilities
{
    /// <summary>
    /// Class which allows the manipulation of in-game memory.
    /// This file provides the implementation for reading/writing of memory.
    /// See the class for the key to the different method names.
    /// </summary>
    public static class StructUtilities
    {
        /// <summary>
        /// Converts a passed in type into a primitive type.
        /// </summary>
        /// <typeparam name="TType">The type to return.</typeparam>
        /// <param name="buffer">The buffer containing the information about the specific type.</param>
        /// <param name="type">The type to convert to (same as type to return.</param>
        /// <returns>In the requested format</returns>
        public static TType ConvertToPrimitive<TType>(ref byte[] buffer, Type type)
        {
            // Convert to user specified structure if none of the types above apply.
            return ArrayToStructure<TType>(ref buffer);
        }

        /// <summary>
        /// Converts a supplied array of bytes into the user passed specified generic struct or class type.
        /// </summary>
        /// <typeparam name="TStructure">A user specified class or structure to convert an array of bytes to.</typeparam>
        /// <param name="bytes">The array of bytes to convert into a specified structure.</param>
        /// <returns>The array of bytes converted to the user's own specified class or structure.</returns>
        public static unsafe TStructure ArrayToStructure<TStructure>(ref byte[] bytes)
        {
            fixed (byte* ptr = &bytes[0])
            {
                try { return (TStructure)Marshal.PtrToStructure((IntPtr)ptr, typeof(TStructure)); }
                catch { return default(TStructure); }
            }
        }

        /// <summary>
        /// Converts a supplied array of bytes into the user passed specified generic struct or class type.
        /// </summary>
        /// <typeparam name="TStructure">A user specified class or structure to convert an array of bytes to.</typeparam>
        /// <param name="bytes">The array of bytes to convert into a specified structure.</param>
        /// <param name="offset">The offset into the structure from which to obtain the structure from.</param>
        /// <returns>The array of bytes converted to the user's own specified class or structure.</returns>
        public static unsafe TStructure ArrayToStructure<TStructure>(ref byte[] bytes, int offset)
        {
            fixed (byte* ptr = &bytes[offset])
            {
                try { return (TStructure)Marshal.PtrToStructure((IntPtr)ptr, typeof(TStructure)); }
                catch { return default(TStructure); }
            }
        }

        /// <summary>
        /// Converts a supplied array of bytes into the user passed specified generic struct or class type.
        /// </summary>
        /// <typeparam name="TStructure">A user specified class or structure to convert an array of bytes to.</typeparam>
        /// <param name="bytes">The array of bytes to convert into a specified structure.</param>
        /// <param name="offset">The offset into the structure from which to obtain the structure from.</param>
        /// <param name="pointer">A number that becomes incremented by the size of the structure when it is read.</param>
        /// <returns>The array of bytes converted to the user's own specified class or structure.</returns>
        public static unsafe TStructure ArrayToStructure<TStructure>(ref byte[] bytes, int offset, ref int pointer)
        {
            fixed (byte* ptr = &bytes[offset])
            {
                try
                {
                    pointer += Marshal.SizeOf<TStructure>();
                    return (TStructure)Marshal.PtrToStructure((IntPtr)ptr, typeof(TStructure));
                }
                catch { return default(TStructure); }
            }
        }

        /// <summary>
        /// Converts a supplied array of bytes into the user passed specified generic struct.
        /// </summary>
        /// <typeparam name="TStructure">A user specified class or structure to convert an array of bytes to.</typeparam>
        /// <param name="bytes">The array of bytes to convert into a specified structure.</param>
        /// <returns>The array of bytes converted to the user's own specified class or structure.</returns>
        public static unsafe TStructure ArrayToStructureUnsafe<TStructure>(ref byte[] bytes)
        {
            fixed (byte* ptr = &bytes[0])
                return Unsafe.Read<TStructure>(ptr);
        }


        /// <summary>
        /// Converts a supplied array of bytes into the user passed specified generic struct.
        /// </summary>
        /// <typeparam name="TStructure">A user specified class or structure to convert an array of bytes to.</typeparam>
        /// <param name="bytes">The array of bytes to convert into a specified structure.</param>
        /// <param name="offset">The offset into the structure from which to obtain the structure from.</param>
        /// <returns>The array of bytes converted to the user's own specified class or structure.</returns>
        public static unsafe TStructure ArrayToStructureUnsafe<TStructure>(ref byte[] bytes, int offset)
        {
            fixed (byte* ptr = &bytes[offset])
                return Unsafe.Read<TStructure>(ptr);
        }

        /// <summary>
        /// Converts a supplied array of bytes into the user passed specified generic struct.
        /// </summary>
        /// <typeparam name="TStructure">A user specified class or structure to convert an array of bytes to.</typeparam>
        /// <param name="bytes">The array of bytes to convert into a specified structure.</param>
        /// <param name="offset">The offset into the structure from which to obtain the structure from.</param>
        /// <param name="pointer">A number that becomes incremented by the size of the structure when it is read.</param>
        /// <returns>The array of bytes converted to the user's own specified class or structure.</returns>
        public static unsafe TStructure ArrayToStructureUnsafe<TStructure>(ref byte[] bytes, int offset, ref int pointer)
        {
            fixed (byte* ptr = &bytes[offset])
            {
                pointer += Unsafe.SizeOf<TStructure>();
                return Unsafe.Read<TStructure>(ptr);
            }
        }

        /// <summary>
        /// Converts a supplied user structure (or class marked [StructLayout(LayoutKind.Sequential)]) into an array of bytes for writing.
        /// </summary>
        /// <param name="structure">The structure to be converted to an array of bytes.</param>
        /// <returns>The user converted structure as an array of bytes.</returns>
        public static byte[] ConvertStructureToByteArray<TStructure>(ref TStructure structure)
        {
            // Retrieve size of structure and allocate buffer.
            int structSize = Marshal.SizeOf(structure);
            byte[] buffer = new byte[structSize];

            // Allocate memory and marshal structure into it.
            IntPtr structPointer = Marshal.AllocHGlobal(structSize);
            Marshal.StructureToPtr(structure, structPointer, true);

            // Copy the structure into our buffer.
            Marshal.Copy(structPointer, buffer, 0, structSize);

            // Free allocated memory and return structure.
            Marshal.FreeHGlobal(structPointer);
            return buffer;
        }

        /// <summary>
        /// Converts a supplied user structure into an array of bytes for writing.
        /// </summary>
        /// <param name="structure">The structure to be converted to an array of bytes.</param>
        /// <returns>The user converted structure as an array of bytes.</returns>
        public static unsafe byte[] ConvertStructureToByteArrayUnsafe<TStructure>(ref TStructure structure)
        {
            byte[] buffer = new byte[Unsafe.SizeOf<TStructure>()];
            fixed (byte* pBuffer = &buffer[0])
                Unsafe.Write(pBuffer, structure);

            return buffer;
        }

        /// <summary>
        /// Writes a supplied user structure (or class marked [StructLayout(LayoutKind.Sequential)])
        /// to a target location in the memory space of the same process.
        /// </summary>
        /// <param name="structure">The structure to be converted to an array of bytes.</param>
        /// <param name="targetAddress">The target address to write the structure contents to.</param>
        /// <returns>The user converted structure as an array of bytes.</returns>
        public static void WriteStructureToAddress<TStructure>(ref TStructure structure, IntPtr targetAddress)
        {
            // Allocate memory and marshal structure into it.
            Marshal.StructureToPtr(structure, targetAddress, true);
        }

        /// <summary>
        /// Writes a supplied user structure to a target location in the memory space of the same process.
        /// </summary>
        /// <param name="structure">The structure to be converted to an array of bytes.</param>
        /// <param name="targetAddress">The target address to write the structure contents to.</param>
        /// <returns>The user converted structure as an array of bytes.</returns>
        public static unsafe void WriteStructureToAddressUnsafe<TStructure>(ref TStructure structure, IntPtr targetAddress)
        {
            Unsafe.Write(targetAddress.ToPointer(), structure);
        }
    }
}