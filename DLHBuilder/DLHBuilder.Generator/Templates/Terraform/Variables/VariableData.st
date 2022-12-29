#####################################################################################################################################
##### Terraform State
###################################################################################################################################

platform_state_key_name             = "<Enter your state_file_name>"
platform_state_container_name       = "<Enter your state_container_name>"
platform_state_storage_account_name = "<Enter your state_storage_account_name>"
platform_state_resource_group_name  = "<Enter your state_resource_group_name>"

###################################################################################################################################
#### Azure Resource
###################################################################################################################################

tenant_id           = "$Environment.TenantID$"
subscription_id     = "$Environment.SubscriptionID$"
azure_short_region  = "uks"
azure_region        = "UK South"
resource_group_name = "$Environment.ResourceGroup$"