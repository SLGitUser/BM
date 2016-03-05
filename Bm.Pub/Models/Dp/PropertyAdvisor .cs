using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Bm.Models.Base;
using Bm.Models.Common;
using Bm.Modules.Orm.Annotation;

namespace Bm.Models.Dp
{
    /// <summary>
    /// ��ҵ����
    /// </summary>
    [DisplayName("��ҵ����")]
    public sealed class PropertyAdvisor : IId, IStamp
    {
        #region Implementation of IId

        /// <summary>
        /// ��ȡ�������ü�¼���
        /// </summary>
        /// <value>
        /// ��¼���
        /// </value>
        /// <remarks>
        /// ����洢ʱʹ���޷�������
        /// </remarks>
        [DisplayName("��¼���")]
        public long Id { get; set; }

        #endregion

        #region Implementation of ICreateStamp

        /// <summary>
        /// ��ȡ�������ü�¼������
        /// </summary>
        /// <value>
        /// ��¼������
        /// </value>
        /// <remarks>
        /// ����ʹ�ò����˵��˻���
        /// </remarks>
        [DisplayName("��¼������")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// ��ȡ�������ü�¼����ʱ��
        /// </summary>
        /// <value>
        /// ��¼����ʱ��
        /// </value>
        /// <remarks>
        /// ����ʹ�÷�����ʱ�䣬��Ҫʹ��Ĭ�Ͽ�ֵ
        /// </remarks>
        [DisplayName("��¼����ʱ��")]
        public DateTime CreatedAt { get; set; }

        #endregion

        #region Implementation of IUpdateStamp

        /// <summary>
        /// ��ȡ�������ü�¼������
        /// </summary>
        /// <value>
        /// ��¼������
        /// </value>
        /// <remarks>
        /// ����ʹ�ò����˵��˻�����Ϊ��ʱ��ʾ��¼�޸���
        /// </remarks>
        [DisplayName("��¼����޸���")]
        public string UpdatedBy { get; set; }

        /// <summary>
        /// ��ȡ�������ü�¼����ʱ��
        /// </summary>
        /// <value>
        /// ��¼����ʱ��
        /// </value>
        /// <remarks>
        /// Ϊ��ʱ��ʾ��¼�޸���
        /// </remarks>
        [DisplayName("��¼����޸�ʱ��")]
        public DateTime? UpdatedAt { get; set; }

        #endregion
        
        /// <summary>
        /// ��ȡ������������
        /// </summary>
        /// <remark></remark>
        [DisplayName("����")]
        [StringLength(50)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// ��ȡ�������ñ��
        /// </summary>
        /// <remark></remark>
        [DisplayName("���")]
        [StringLength(36)]
        public string No { get; set; }

        /// <summary>
        /// ��ȡ���������ֻ�����
        /// </summary>
        /// <remark></remark>
        [DisplayName("�ֻ�����")]
        [StringLength(11)]
        [Required]
        public string MobileNo { get; set; }

        /// <summary>
        /// ��ȡ��������ְλ
        /// </summary>
        /// <remark></remark>
        [DisplayName("ְλ")]
        [StringLength(20)]
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// ��ȡ��������¥�̱��
        /// </summary>
        /// <remark></remark>
        [DisplayName("¥��")]
        [StringLength(50)]
        [Required]
        public string ProjectNo { get; set; }

        /// <summary>
        /// �˻���Ϣ
        /// </summary>
        [IgnoreMapping]
        public Account Account { get; set; }

    }
}
