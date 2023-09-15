import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44344/',
  redirectUri: baseUrl,
  clientId: 'Vehman2_App',
  responseType: 'code',
  scope: 'offline_access Vehman2',
  requireHttps: true,
};

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'Vehman2',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44344',
      rootNamespace: 'Vehman2',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
  remoteEnv: {
    url: '/getEnvConfig',
    mergeStrategy: 'deepmerge'
  }
} as Environment;
