using UnityEditorInternal.Profiling.Memory.Experimental.FileFormat;
        
        
namespace MemorySnapshotConvertor
{
    public class MemorySnapshotConvertorWriter
    {
        public void WriteToFile(string path, PackedMemorySnapshot legacySnapshot)
        {

        }

        private void WriteEntry(EntryType entryType, string data)
        {

        }
        private void WriteEntry<T>(EntryType entryType, T data) where T : struct
        {

        }
        private void WriteEntryArray<T>(EntryType entryType, T[] data) where T : struct
        {

        }
    }
}


namespace UnityEditorInternal.Profiling.Memory.Experimental.FileFormat
{
    public enum EntryType : ushort
    {
        Metadata_Version = 0,
        Metadata_RecordDate = 1,
        Metadata_UserMetadata = 2,
        Metadata_CaptureFlags = 3,
        Metadata_VirtualMachineInformation = 4,
        NativeTypes_Name = 5,
        NativeTypes_NativeBaseTypeArrayIndex = 6,
        NativeObjects_NativeTypeArrayIndex = 7,
        NativeObjects_HideFlags = 8,
        NativeObjects_Flags = 9,
        NativeObjects_InstanceId = 10,
        NativeObjects_Name = 11,
        NativeObjects_NativeObjectAddress = 12,
        NativeObjects_Size = 13,
        NativeObjects_RootReferenceId = 14,
        GCHandles_Target = 15,
        Connections_From = 16,
        Connections_To = 17,
        ManagedHeapSections_StartAddress = 18,
        ManagedHeapSections_Bytes = 19,
        ManagedStacks_StartAddress = 20,
        ManagedStacks_Bytes = 21,
        TypeDescriptions_Flags = 22,
        TypeDescriptions_Name = 23,
        TypeDescriptions_Assembly = 24,
        TypeDescriptions_FieldIndices = 25,
        TypeDescriptions_StaticFieldBytes = 26,
        TypeDescriptions_BaseOrElementTypeIndex = 27,
        TypeDescriptions_Size = 28,
        TypeDescriptions_TypeInfoAddress = 29,
        TypeDescriptions_TypeIndex = 30,
        FieldDescriptions_Offset = 31,
        FieldDescriptions_TypeIndex = 32,
        FieldDescriptions_Name = 33,
        FieldDescriptions_IsStatic = 34,
        NativeRootReferences_Id = 35,
        NativeRootReferences_AreaName = 36,
        NativeRootReferences_ObjectName = 37,
        NativeRootReferences_AccumulatedSize = 38,
        NativeAllocations_MemoryRegionIndex = 39,
        NativeAllocations_RootReferenceId = 40,
        NativeAllocations_AllocationSiteId = 41,
        NativeAllocations_Address = 42,
        NativeAllocations_Size = 43,
        NativeAllocations_OverheadSize = 44,
        NativeAllocations_PaddingSize = 45,
        NativeMemoryRegions_Name = 46,
        NativeMemoryRegions_ParentIndex = 47,
        NativeMemoryRegions_AddressBase = 48,
        NativeMemoryRegions_AddressSize = 49,
        NativeMemoryRegions_FirstAllocationIndex = 50,
        NativeMemoryRegions_NumAllocations = 51,
        NativeMemoryLabels_Name = 52,
        NativeAllocationSites_Id = 53,
        NativeAllocationSites_MemoryLabelIndex = 54,
        NativeAllocationSites_CallstackSymbols = 55,
        NativeCallstackSymbol_Symbol = 56,
        NativeCallstackSymbol_ReadableStackTrace = 57
    }
}