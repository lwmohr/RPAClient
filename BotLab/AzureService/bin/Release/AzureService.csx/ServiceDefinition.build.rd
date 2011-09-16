<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="CompeteTest" generation="1" functional="0" release="0" Id="ddb8a156-708a-41c4-8bc7-0bb6494f116e" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="CompeteTestGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="BotLabSite:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/CompeteTest/CompeteTestGroup/LB:BotLabSite:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="BotLabSite:?IsSimulationEnvironment?" defaultValue="">
          <maps>
            <mapMoniker name="/CompeteTest/CompeteTestGroup/MapBotLabSite:?IsSimulationEnvironment?" />
          </maps>
        </aCS>
        <aCS name="BotLabSite:?RoleHostDebugger?" defaultValue="">
          <maps>
            <mapMoniker name="/CompeteTest/CompeteTestGroup/MapBotLabSite:?RoleHostDebugger?" />
          </maps>
        </aCS>
        <aCS name="BotLabSite:?StartupTaskDebugger?" defaultValue="">
          <maps>
            <mapMoniker name="/CompeteTest/CompeteTestGroup/MapBotLabSite:?StartupTaskDebugger?" />
          </maps>
        </aCS>
        <aCS name="BotLabSiteInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/CompeteTest/CompeteTestGroup/MapBotLabSiteInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:BotLabSite:Endpoint1">
          <toPorts>
            <inPortMoniker name="/CompeteTest/CompeteTestGroup/BotLabSite/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapBotLabSite:?IsSimulationEnvironment?" kind="Identity">
          <setting>
            <aCSMoniker name="/CompeteTest/CompeteTestGroup/BotLabSite/?IsSimulationEnvironment?" />
          </setting>
        </map>
        <map name="MapBotLabSite:?RoleHostDebugger?" kind="Identity">
          <setting>
            <aCSMoniker name="/CompeteTest/CompeteTestGroup/BotLabSite/?RoleHostDebugger?" />
          </setting>
        </map>
        <map name="MapBotLabSite:?StartupTaskDebugger?" kind="Identity">
          <setting>
            <aCSMoniker name="/CompeteTest/CompeteTestGroup/BotLabSite/?StartupTaskDebugger?" />
          </setting>
        </map>
        <map name="MapBotLabSiteInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/CompeteTest/CompeteTestGroup/BotLabSiteInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="BotLabSite" generation="1" functional="0" release="0" software="L:\RPAClient\BotLab\AzureService\bin\Release\AzureService.csx\roles\BotLabSite" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWebHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="?IsSimulationEnvironment?" defaultValue="" />
              <aCS name="?RoleHostDebugger?" defaultValue="" />
              <aCS name="?StartupTaskDebugger?" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;BotLabSite&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;BotLabSite&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="localdisk" defaultAmount="[2048,2048,2048]" defaultSticky="false" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/CompeteTest/CompeteTestGroup/BotLabSiteInstances" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyID name="BotLabSiteInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="3239476d-417d-4d51-af65-3f1e0e034852" ref="Microsoft.RedDog.Contract\ServiceContract\CompeteTestContract@ServiceDefinition.build">
      <interfacereferences>
        <interfaceReference Id="4d956be3-9a0e-4935-bbfe-f9e876d9387d" ref="Microsoft.RedDog.Contract\Interface\BotLabSite:Endpoint1@ServiceDefinition.build">
          <inPort>
            <inPortMoniker name="/CompeteTest/CompeteTestGroup/BotLabSite:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>