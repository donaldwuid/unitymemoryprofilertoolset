
## First things first

com.unity.memoryprofiler: new il2cpp-based memoryprofiler v2 for Unity 2018.

BitbucketMemoryProfiler: old il2cpp-based memoryprofiler for Unity5/2017.


If you are using Unity2018,
- Copy com.unity.memoryprofiler into your Unity2018 project's **Assets/StandardAssets/** folder. Empty project is also fine.
- That's it! Connect your device, capture and profile!


If you are using Unity5 or Unity2017,
- Copy com.unity.memoryprofiler into your Unity2018 project's **Assets/StandardAssets/** folder. Empty project is also fine.
- You should copy BitbucketMemoryProfiler into your Unity5/2017 project as well. Empty project is also fine.
- Open BitbucketMemoryProfiler panel in your Unity5/2017 project, in order to save your time, make sure **Enable Crawl** to be toggle off!
- Connect your device, capture and save your device memory snapshot **.memsnap3** file
- Then, in your Unity2018 project, use the com.unity.memoryprofiler to import your memory snapshot.

This com.unity.memoryprofiler and BitbucketMemoryProfiler is modified to fit this workflow, and for some other improvements.


## Unity内存Profile方法汇总


当下（2019年4月），在以iOS设备为例的真机上进行Unity内存占用分析，主要有4种Profile方法：
- XCode Memory Graph + Malloc Stack
- Unity Built-in Memory Profiler（所有Unity）
- Unity Bitbucket Memory Profiler（Unity 5.3 ~ Unity 2017.4）
- Unity New Memory Profiler v2（Unity 2018）

它们有自己的优缺点，分析如下。

### XCode Memory Graph + Malloc Stack
XCode Memory Graph<sup>[1]</sup>在开启Malloc Stack选项的情况下，抓取到的memgraph文件，配合`vmmap`、`malloc_history`等命令，可以提供非常详细的操作系统Native级别、Metal图形驱动级别的内存分配信息。
下面是抓取结果示例，

|内存类型|内存大小（Dirty Size + Swapped Size）|
|--|--|
|物理内存占用约：|500.7MB（258.7M + 242.0M）|
|MALLOC_LARGE|145.4MB（47.7M + 97.7M）|
|IOKIT|113.3MB（64.4M + 48.9M）|
|MALLOC_SMALL|79.1MB（20.0M + 59.1M）|
|VM_ALLOCATE|55.8MB（46.9M + 9152K）|
|MALLOC_NANO|39.9MB（27.6M + 12.3M）|
|MALLOC_TINY|36.0MB（32.3M + 3760K）|
|IOSurface|11.8MB（11.8M + 16K）|

能针对上面内存概况进行一定程度的细分数据分析及堆栈抓取，举例如下：

|内存申请描述|内存大小
|--|--|
|wwise加载Bank|11.5MB+2.34MB+2MB|
|CmdQ的初始化|8.1MB|
|C#反射信息TABLE的扩容|8MB+6MB+6MB|
|IOKit/ConstantBuffer/ScratchBuffer|4MB+4MB+4MB+4mb|


但从上面的XCode抓取Memory Graph可以看出，其提供的信息都是操作系统Native级别、Metal图形驱动级别的内存分配，
但Unity一般是申请了大块Native内存后，自行在里面进行内存管理，其对于Native内存来说是黑盒。
所以，XCode抓取Memory Graph粒度太粗，如能有精确逻辑意义的Unity object level的信息（C# object、engine object如texture data object等），将事半功倍。

### Unity Built-in Memory Profiler
Unity Built-in Memory Profiler在所有Unity都支持，兼容性很好，且提供了比较好的Unity engine object的信息，但其对C# object来说依然比较笼统，且不能给出精确的Unity object之间的引用关系。也不能进行内存快照对比、分析内存泄漏情况。

### Unity Bitbucket Memory Profiler
Unity自5.3版本开始，提供了Bitbucket Memory Profiler<sup>[2]</sup>。
它的优点也比较明显，其依赖于Unity自己掌控的IL2CPP，结合Unity的内部内存管理，抓取出详尽的Memory Snapshot，提供了具体的Unity object信息（C# object、engine object）。
但是！慢！太慢！是Unity Bitbucket Memory Profiler的致命缺点！谁也不愿意忍受30分钟乃至几个小时的等待时间！这是因为它当前的实现，是拿到Snapshot后，进行全量的内存数据分析（Crawling），才能进行展示。
其数据呈现默认只支持Tree Map，不利于海量对象的分析。
所幸这个Profiler的分析器和Editor的源码是公开的，所以理论上可以修改其分析器源码，改进其全量分析导致耗时等缺点。

### Unity Memory Profiler V2
在打算修改Bitbucket Memory Profiler源码前，理应了解一下当前有否已有类似工程的存在。
在这过程中，发现Unity 2018有新的Memory Profiler v2的Preview<sup>[3]</sup>，其改进了Bitbucket Memory Profiler的慢的缺点，而且提供了snapshot的对比工具以分析泄漏情况，其Editor在数据呈现上也比较人性化，除了TreeMap外，提供了List View、Memory Map、Table View等。
它的兼容性本是个问题，因其本需要2018.3内置新的Memory Snapshot Backend以抓取新的SnapshotV2数据结构才能工作。但后面新的版本考虑了兼容性，支持将Bitbucket Memory Profiler抓取的Snapshot转为SnapshotV2，从而可以在Unity 5.3~2017.4进行Snapshot抓取、在2018进行分析。


### 小结
总而言之，Unity一般是申请了大块Native内存后，自行在里面进行内存管理，包括进行Engine Object的分配的和C# Object的分配。
XCode Memory Graph只能给出操作系统Native级别、Metal驱动级别的内存分配，粒度太粗，只能进行宏观分析。
只有Unity的Profiler才能进行内存Object-Level的精确分析。Unity Built-in Memory Profiler未做到C# Object-Level精度、难于进行对比泄漏分析。
Unity Bitbucket Memory Profiler的抓取Memory Snapshot的速度很快，但其数据分析（Crawling）实现是全量分析，导致速度慢得难以接受。
Unity New Memory Profiler V2是Unity 2018新的Memory Profiler，其增量地进行数据分析，且提供了内存快照对比查找泄漏，而且兼容Bitbucket Memory Profiler的抓取Memory Snapshot，所以是当前最适合的内存Profiler工具。

## 引用
[1] WWDC, *"iOS Memory Deep Dive"*, 2018. Available: https://developer.apple.com/videos/play/wwdc2018/416/

[2] Unity, *"MemoryProfiler"*, 2017. Available: https://bitbucket.org/Unity-Technologies/memoryprofiler

[3] Unity, *"New Memory Profiler preview package available for Unity 2018.3"*, 2018. Available: https://forum.unity.com/threads/new-memory-profiler-preview-package-available-for-unity-2018-3.597271/
