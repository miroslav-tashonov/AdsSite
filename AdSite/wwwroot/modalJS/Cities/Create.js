﻿<div class="container">
    <div class="modal fade" tabindex="-1" id="loginModal"
        data-keyboard="false" data-backdrop="static">
        <div class="modal-dialog modal-md">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">
                        ×
                    </button>
                    <h4 class="modal-title"><localize name="City_CreateNew"></localize></h4>
                </div>
                <form asp-action="Create">
                    <div class="modal-body">

                        <div asp-validation-summary="ModelOnly" class="text-danger"></div>
                        <div class="form-group">
                            <label class="control-label"><localize name="City_Field_Name"></localize></label>
                            <input asp-for="Name" class="form-control" />
                            <span asp-validation-for="Name" class="text-danger"></span>
                        </div>
                        <div class="form-group">
                            <label class="control-label"><localize name="City_Field_Postcode"></localize></label>
                            <input asp-for="Postcode" class="form-control" />
                            <span asp-validation-for="Postcode" class="text-danger"></span>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="submit" class="btn btn-default"><localize name="General_Create"></localize></button>
                        <a asp-action="Index" class="btn btn-default"><localize name="General_BackToList"></localize></a>
                    </div>
                </form>
            </div>
        </div>
    </div>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        $("#loginModal").modal('show');
    });
</script>