namespace backend.Dto;

public class MsgStatus{
    public MsgStatus(string message, int status){
        Message = message;
        StatusCode = status;
    }
    public string? Message { get; set; }
    public int? StatusCode { get; set; }
}