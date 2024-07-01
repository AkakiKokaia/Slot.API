namespace Project.Shared.Configuration.Wrappers;

public record PagedRequestModel(int pageNumber = 0, int pageSize = 25);
