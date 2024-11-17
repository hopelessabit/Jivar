namespace Jivar.BO.Enumarate
{
    public enum ProjectRoleType
    {
        Owner,          // Full control over the project
        Admin,          // Can manage settings and members
        Manager,        // Can manage tasks, sprints, and workflows
        Developer,      // Can work on tasks and update progress
        Tester,         // Can test tasks and provide feedback
        Viewer          // Read-only access to the project
    }

}
