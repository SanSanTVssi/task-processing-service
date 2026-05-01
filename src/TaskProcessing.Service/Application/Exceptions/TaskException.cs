using TaskProcessing.Service.Domain;

namespace TaskProcessing.Service.Application.Exceptions;

public class TaskException(string? msg) : Exception(msg);
public class TaskStateTransitionException(string? msg) : Exception(msg);
public class TaskNotFoundException() : TaskException("Task not found");
public class InvalidTaskStateTransitionException(TaskEntityStatus from, TaskEntityStatus to) 
    : TaskStateTransitionException($"Cannot change task status from {from} to {to}");

