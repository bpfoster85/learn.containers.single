# learn-containers
Repo to strengthen the containers muscle


## Setup Container Apps Environment
  - Install the container app extension \
  `az extension add --source https://workerappscliextension.blob.core.windows.net/azure-cli-extension/containerapp-0.2.0-py2.py3-none-any.whl`
  - Register namespace \
  `az provider register --namespace Microsoft.Web`
  - Set up Log Analytics workspace for monitoring container app \
  `az monitor log-analytics workspace create \
  --resource-group $RESOURCE_GROUP \
  --workspace-name $LOG_ANALYTICS_WORKSPACE`
  - need the following information for the container app monitoring \
  `az monitor log-analytics workspace show --query customerId -g $RESOURCE_GROUP -n $LOG_ANALYTICS_WORKSPACE --out tsv` \
  `az monitor log-analytics workspace get-shared-keys --query primarySharedKey -g $RESOURCE_GROUP -n $LOG_ANALYTICS_WORKSPACE --out tsv` 
 - Create container App environment \
 `az containerapp env create \
  --name $CONTAINERAPPS_ENVIRONMENT \
  --resource-group $RESOURCE_GROUP \
  --logs-workspace-id $LOG_ANALYTICS_WORKSPACE_CLIENT_ID \
  --logs-workspace-key $LOG_ANALYTICS_WORKSPACE_CLIENT_SECRET \
  --location $LOCATION`
  
## Create a Container App now that the environment is built out 
`az containerapp create \
  --name my-container-app \
  --resource-group $RESOURCE_GROUP \
  --environment $CONTAINERAPPS_ENVIRONMENT \
  --image mcr.microsoft.com/azuredocs/containerapps-helloworld:latest \
  --target-port 80 \
  --ingress 'external' \
  --query configuration.ingress.fqdn \
  --registry-login-server \
  --registry-password \
  --registry-username`

## Build Dockerfile Image
docker login $ACRNAME.azurecr.io \
cd mountainbikeapi \
docker build --tag $TAGNAME . \
docker push $ACRNAME.azurecr.io/$TAGNAME  \
