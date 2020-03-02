# EFPlusIssue581

https://github.com/zzzprojects/EntityFramework-Plus/issues/581#issuecomment-592981695

With package Z.EntityFramework.Plus.EFCore version 3.0.30 the unit test works.


With package Z.EntityFramework.Plus.EFCore version 3.0.40 the unit test fails:
```
  Message: 
    System.InvalidOperationException : The instance of entity type 'Device' cannot be tracked because another instance with the key value '{Id: 8de3c1b0-75c4-4e1c-8b18-8a99b99019f3}' is already being tracked. When attaching existing entities, ensure that only one entity instance with a given key value is attached.
  Stack Trace: 
    IdentityMap`1.ThrowIdentityConflict(InternalEntityEntry entry)
    IdentityMap`1.Add(TKey key, InternalEntityEntry entry, Boolean updateDuplicate)
    IdentityMap`1.Add(TKey key, InternalEntityEntry entry)
    IdentityMap`1.Add(InternalEntityEntry entry)
    StateManager.StartTracking(InternalEntityEntry entry)
    InternalEntityEntry.SetEntityState(EntityState oldState, EntityState newState, Boolean acceptChanges, Boolean modifyProperties)
    InternalEntityEntry.SetEntityState(EntityState entityState, Boolean acceptChanges, Boolean modifyProperties, Nullable`1 forceStateWhenUnknownKey)
    EntityGraphAttacher.PaintAction(EntityEntryGraphNode`1 node)
    EntityEntryGraphIterator.TraverseGraph[TState](EntityEntryGraphNode`1 node, Func`2 handleNode)
    EntityGraphAttacher.AttachGraph(InternalEntityEntry rootEntry, EntityState targetState, EntityState storeGeneratedWithKeySetTargetState, Boolean forceStateWhenUnknownKey)
    DbContext.SetEntityState(InternalEntityEntry entry, EntityState entityState)
    DbContext.SetEntityState[TEntity](TEntity entity, EntityState entityState)
    DbContext.Attach[TEntity](TEntity entity)
    BatchUpdate.Execute[T](IQueryable`1 query, Expression`1 updateFactory)
    BatchUpdateExtensions.UpdateFromQuery[T](IQueryable`1 query, Expression`1 updateFactory, Action`1 batchUpdateBuilder)
    BatchUpdateExtensions.Update[T](IQueryable`1 query, Expression`1 updateFactory, Action`1 batchUpdateBuilder)
    <>c__DisplayClass2_0`1.<UpdateAsync>b__0()
    Task`1.InnerInvoke()
    <.cctor>b__274_0(Object obj)
    ExecutionContext.RunFromThreadPoolDispatchLoop(Thread threadPoolThread, ExecutionContext executionContext, ContextCallback callback, Object state)
    --- End of stack trace from previous location where exception was thrown ---
    ExecutionContext.RunFromThreadPoolDispatchLoop(Thread threadPoolThread, ExecutionContext executionContext, ContextCallback callback, Object state)
    Task.ExecuteWithThreadLocal(Task& currentTaskSlot, Thread threadPoolThread)
    --- End of stack trace from previous location where exception was thrown ---
    Tests.Test1() line 75
    GenericAdapter`1.BlockUntilCompleted()
    NoMessagePumpStrategy.WaitForCompletion(AwaitAdapter awaitable)
    AsyncToSyncAdapter.Await(Func`1 invoke)
    TestMethodCommand.RunTestMethod(TestExecutionContext context)
    TestMethodCommand.Execute(TestExecutionContext context)
    <>c__DisplayClass1_0.<Execute>b__0()
    BeforeAndAfterTestCommand.RunTestMethodInThreadAbortSafeZone(TestExecutionContext context, Action action)
```
